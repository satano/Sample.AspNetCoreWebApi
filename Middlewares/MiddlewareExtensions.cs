using Kros.Utils;
using Microsoft.AspNetCore.Builder;

namespace Sample.AspNetCoreWebApi.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder appBuilder)
        {
            Check.NotNull(appBuilder, nameof(appBuilder));

            return appBuilder.UseMiddleware<LoggingMiddleware>();
        }
    }
}