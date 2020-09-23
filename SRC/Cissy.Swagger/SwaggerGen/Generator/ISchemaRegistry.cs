using System;
using System.Collections.Generic;
using Cissy.Swagger;

namespace Cissy.SwaggerGen
{
    public interface ISchemaRegistry
    {
        Schema GetOrRegister(Type type);

        IDictionary<string, Schema> Definitions { get; }
    }
}
