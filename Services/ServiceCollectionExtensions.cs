using System.Configuration;
using Kros.KORM;
using Kros.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sample.AspNetCoreWebApi.Authorization;
using Sample.AspNetCoreWebApi.Models;

namespace Sample.AspNetCoreWebApi.Services
{
    public static class ServiceCollectionExtensions
    {
        private const string ConnectionStringSectionName = "ConnectionString";
        private const string CheckNonActiveSearchesOptionsSectionName = "CheckNonActiveSearchesOptions";

        /// <summary>
        /// Register KORM to IoC container.
        /// </summary>
        /// <param name="services">IoC container.</param>
        public static IServiceCollection AddKorm(this IServiceCollection services, IConfiguration configuration)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(configuration, nameof(configuration));

            var connectionString = configuration.GetSection(ConnectionStringSectionName).Get<ConnectionStringSettings>();

            return services.AddScoped<IDatabase>((serviceProvider) =>
            {
                return new Database(connectionString);
            });
        }

        /// <summary>
        /// Register KORM to IoC container.
        /// </summary>
        /// <param name="services">IoC container.</param>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            return services.AddTransient<IPeopleRepository, PeopleRepository>()
                .AddTransient<IUserRepository, UserRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            return services;
        }

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
                    policy => policy.RequireClaim(configuration["Authentication:UserClaimName"]));
            });

            return services;
        }

    }
}