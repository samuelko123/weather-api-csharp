using System;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

using WeatherService.Core;
using Swashbuckle.AspNetCore.SwaggerUI;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace WeatherService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // Add heading to Swagger Doc
                c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Weather Service",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Samuel Ko",
                        Url = new Uri("https://samuelko123.github.io/"),
                        Email = String.Empty,
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                // Convert parameters to lowercase in Swagger Doc
                c.DocumentFilter<LowerCaseDocumentFilter>();

                // Tell Swagger to use XML comments generated from ASP.NET, e.g. param example
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                options.JsonSerializerOptions.WriteIndented = true;
            });
            services.AddHttpClient(Constants.OpenWeatherMap, httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.ConfigObject = new ConfigObject
                {
                    // Show validation in Swagger, e.g. min, max
                    ShowCommonExtensions = true
                };
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}