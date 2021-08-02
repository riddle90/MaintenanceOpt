using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Common
{
    public static class SwaggerExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services, SwaggerApiDefinition apiDefinition)
        {
            services.AddSwaggerGen(options =>
            {
                //options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc(apiDefinition.ApiVersion,
                    new OpenApiInfo {Title = apiDefinition.ApiName, Version = apiDefinition.ApiVersion});

                // Set the comments path for the Swagger JSON and UI.
                var xmlPath = Path.Combine(AppContext.BaseDirectory, apiDefinition.ApiXmlName);
                options.IncludeXmlComments(xmlPath);

            });
        }

        public static void EnableSwagger(this IApplicationBuilder app, SwaggerApiDefinition apiDefinition)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(apiDefinition.SwaggerUrl,
                    $"{apiDefinition.ApiName} {apiDefinition.ApiVersion}");
                options.DisplayRequestDuration();
            });
        }
    }
}