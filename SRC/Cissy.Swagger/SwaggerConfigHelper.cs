using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
//using Cissy.SwaggerUI;
//using Cissy.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Cissy.Configuration;

namespace Cissy.Swagger
{
    /// <summary>
    /// swagger配置帮助
    /// </summary>
    public static class SwaggerConfigHelper
    {
        static SwaggerConfig _waggerConfig;
        public static CissyConfigBuilder AddCissySwaggerConfig(this CissyConfigBuilder cissyConfigBuilder, Action<SwaggerGenOptions> setupAction = null)
        {
            ICissyConfig cissyConfig = cissyConfigBuilder.CissyConfig;
            if (cissyConfig.IsNotNull())
            {
                SwaggerConfig swaggerConfig = cissyConfig.GetConfig<SwaggerConfig>();
                if (swaggerConfig.IsNotNull())
                {
                    _waggerConfig = swaggerConfig;
                    cissyConfigBuilder.ServiceCollection.AddSwaggerGen(c =>
                    {
                        List<OpenApiTag> tags = new List<OpenApiTag>();
                        foreach (SwaggerVersionConfig version in swaggerConfig.Versions)
                        {
                            c.SwaggerDoc(version.Version, new OpenApiInfo
                            {
                                Version = version.Version,
                                Title = version.Title,
                                Description = version.Description,
                                TermsOfService = version.TermsOfService.IsNotNullAndEmpty() ? new Uri(version.TermsOfService) : default(Uri)
                            });
                        }
                        c.DocInclusionPredicate((docName, apiDesc) =>
                        {
                            if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                            var versions = methodInfo.DeclaringType
                                .GetCustomAttributes(true)
                                .OfType<CissyApiAttribute>();
                            bool ok = versions.Any(v => v.GroupName.IsNullOrEmpty() || v.GroupName == docName);
                            if (ok)
                            {
                                foreach (CissyApiAttribute caa in versions)
                                {
                                    if (caa.Description.IsNotNullAndEmpty())
                                    {
                                        var name = methodInfo.DeclaringType.Name;
                                        var lname = name.ToLower();
                                        string ctn = "controller";
                                        if (lname.EndsWith(ctn))
                                            name = name.Substring(0, name.Length - ctn.Length);
                                        tags.Add(new OpenApiTag()
                                        {
                                            Name = name,
                                            Description = caa.Description
                                        });
                                    }
                                }
                            }
                            return ok;
                        });
                        DefaultControllerTagDescriptions.SetTags(tags);
                        c.DocumentFilter<DefaultControllerTagDescriptions>();
                        setupAction?.Invoke(c);
                    });
                }
            }
            return cissyConfigBuilder;
        }
        public static IApplicationBuilder UseCissySwagger(this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "apidoc";
                foreach (SwaggerVersionConfig version in _waggerConfig.Versions)
                {
                    c.SwaggerEndpoint($"/swagger/{version.Version}/swagger.json", $"{version.Title} {version.Version}");
                }
                ////注入汉化文件
                //c.InjectJavascript($"/cissy/core/resource/js/swaggercn.js");
            });
            return app;
        }
    }
}
