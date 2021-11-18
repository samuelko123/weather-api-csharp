using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace WeatherService.Core
{
    public class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            Dictionary<string, OpenApiPathItem> paths = swaggerDoc.Paths.ToDictionary(
                entry => string.Join('/', entry.Key.Split('/').Select(x => x.ToLower())),
                entry => entry.Value
            );

            swaggerDoc.Paths = new OpenApiPaths();
            foreach ((string key, OpenApiPathItem value) in paths)
            {
                foreach (OpenApiParameter param in value.Operations.SelectMany(o => o.Value.Parameters))
                {
                    param.Name = param.Name.ToLower();
                }

                swaggerDoc.Paths.Add(key, value);
            }
        }
    }

}