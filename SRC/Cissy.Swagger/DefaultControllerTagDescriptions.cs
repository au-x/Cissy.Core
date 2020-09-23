using System;
using System.Collections.Generic;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
namespace Cissy.Swagger
{
    internal class DefaultControllerTagDescriptions : IDocumentFilter
    {
        static List<OpenApiTag> Tags;
        public static void SetTags(List<OpenApiTag> tags)
        {
            Tags = tags;
        }

        /// <summary>
        /// apply
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = Tags;
        }
    }
}
