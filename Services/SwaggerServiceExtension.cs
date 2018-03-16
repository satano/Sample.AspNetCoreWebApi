using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Sample.AspNetCoreWebApi.Services
{
    /// <summary>
    /// Extensions for adding swagger documentation to IoC and middleware.
    /// </summary>
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// Add swagger to IoC container.
        /// </summary>
        /// <param name="services">IoC container.</param>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                        Title = "People API",
                        Description = "A simple example ASP.NET Core Web API",
                        Contact = new Contact
                        {
                            Name = "Miňo Martiniak",
                                Url = "https://twitter.com/MinoMartiniak"
                        }
                });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Sample.AspNetCoreWebApi.xml");
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                });
            });

        /// <summary>
        /// Add swagger to middlewares.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app) =>
            app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "People API");
            });
    }
}