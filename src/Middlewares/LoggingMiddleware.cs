using System.Diagnostics;
using System.Threading.Tasks;
using Kros.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Sample.AspNetCoreWebApi.Middlewares
{
    /// <summary>
    /// Request middleware for logging request duration.
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="next">Next request delegate.</param>
        /// <param name="logger">Logger.</param>
        public LoggingMiddleware(
            RequestDelegate next,
            ILogger<LoggingMiddleware> logger)
        {
            _logger = Check.NotNull(logger, nameof(logger));
            _next = Check.NotNull(next, nameof(next));
        }

        public Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();

            var ret = this._next(context);

            sw.Stop();
            _logger.LogInformation("Request duration: {0}ms.", sw.ElapsedMilliseconds);

            return ret;
        }
    }
}
