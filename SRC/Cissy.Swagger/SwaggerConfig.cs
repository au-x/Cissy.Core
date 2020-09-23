using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Cissy.Configuration;
namespace Cissy.Swagger
{
    /// <summary>
    /// swagger配置
    /// </summary>
    public class SwaggerConfig : IConfigModel
    {
        public string ConfigName { get { return "swagger"; } }
        public List<SwaggerVersionConfig> Versions = new List<SwaggerVersionConfig>();
        public void InitConfig(IConfigurationSection section)
        {
            foreach (IConfigurationSection dbsection in section.GetChildren())
            {
                SwaggerVersionConfig connectionConfig = new SwaggerVersionConfig();
                connectionConfig.InitConfig(dbsection);
                Versions.Add(connectionConfig);
            }
        }
    }
    public class SwaggerVersionConfig : IConfigModel
    {
        public string ConfigName { get { return "swaggerversion"; } }
        public string Version;
        public string Title;
        public string Description;
        public string TermsOfService;
        public void InitConfig(IConfigurationSection section)
        {
            Version = section["version"];
            Title = section["title"];
            Description = section["description"];
            TermsOfService = section["termsofservice"];
        }
    }
}
