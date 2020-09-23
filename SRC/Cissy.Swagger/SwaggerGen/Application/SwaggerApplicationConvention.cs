using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Cissy.SwaggerGen
{
    public class SwaggerApplicationConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            application.ApiExplorer.IsVisible = true;
        }
    }
}