using Kros.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Sample.AspNetCoreWebApi.Authorization;
using Sample.AspNetCoreWebApi.Models;
using System.Security.Claims;

namespace Sample.AspNetCoreWebApi.Services
{
    /// <summary>
    /// Extensions for adding services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register KORM to IoC container.
        /// </summary>
        /// <param name="services">IoC container.</param>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            services.TryAddTransient<IPeopleRepository, PeopleRepository>();
            services.TryAddTransient<IUserRepository, UserRepository>();

            return services;
        }

        /// <summary>
        /// Register services to IoC container.
        /// </summary>
        /// <param name="services">IoC container.</param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            services.TryAddTransient<IActiveUser, HttpContextUser>();

            return services;
        }

        /// <summary>
        /// Register Jwt authorization to IoC conteiner
        /// </summary>
        /// <param name="services">IoC container.</param>
        /// <param name="configuration">Jwt configuration.</param>
        /// <returns>IoC container for fluent syntax.</returns>
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationOption>(configuration.GetSection("Authentication"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = configuration["Authentication:Issuer"],
                        ValidAudience = configuration["Authentication:Audience"],
                        IssuerSigningKey = JwtToken.GetSecret(configuration["Authentication:Key"])
                    };

                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtToken.ADMIN_NAME,
                    policy => policy.RequireClaim(configuration["Authentication:AdminClaimName"]));
                options.AddPolicy(JwtToken.USER_NAME,
                    policy => policy.RequireClaim(ClaimTypes.Sid));
            });

            return services;
        }

    }
}