using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kros.KORM.Extensions.Asp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.AspNetCoreWebApi.Middlewares;
using Sample.AspNetCoreWebApi.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Sample.AspNetCoreWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddKorm(Configuration);
            services.AddRepositories()
                .AddJwtAuthorization(Configuration)
                .AddDirectoryBrowser()
                .AddResponseCompression()
                .AddSwaggerDocumentation()
                .AddServices()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy")
                .Map("/api/test", TestApi)
                .UseResponseCompression()
                .UseStaticFiles()
                .UseFileServer(new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                    RequestPath = "/StaticFiles",
                    EnableDirectoryBrowsing = true
                })
                .UseSwaggerDocumentation()
                .UseAuthentication()
                .UseLogging()
                .UseMvc();
        }

        private static void TestApi(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("true");
            });
        }

    }
}