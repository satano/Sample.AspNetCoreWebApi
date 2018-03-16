using Kros.Utils;
using Microsoft.AspNetCore.Builder;

namespace Sample.AspNetCoreWebApi.Middlewares
{
    /// <summary>
    /// Extensions for adding Middlewares.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Use Logging middlware.
        /// </summary>
        /// <param name="appBuilder">Application builder.</param>
        /// <returns>
        /// Application builder for fluent syntaxt.
        /// </returns>
        public static IApplicationBuilder UseLogging(this IApplicationBuilder appBuilder)
        {
            Check.NotNull(appBuilder, nameof(appBuilder));

            return appBuilder.UseMiddleware<LoggingMiddleware>();
        }
    }
}