﻿using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Cissy.Swagger;

namespace Cissy.SwaggerGen
{
    public interface IOperationFilter
    {
        void Apply(Operation operation, OperationFilterContext context);
    }

    public class OperationFilterContext
    {
        public OperationFilterContext(
            ApiDescription apiDescription,
            ISchemaRegistry schemaRegistry,
            MethodInfo methodInfo)
        {
            ApiDescription = apiDescription;
            SchemaRegistry = schemaRegistry;
            MethodInfo = methodInfo;
        }

        public ApiDescription ApiDescription { get; private set; }

        public ISchemaRegistry SchemaRegistry { get; private set; }

        public MethodInfo MethodInfo { get; }
    }
}
