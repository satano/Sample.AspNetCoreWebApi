using System.Linq;
using System.Security.Claims;
using Kros.Utils;
using Microsoft.AspNetCore.Http;

namespace Sample.AspNetCoreWebApi.Services
{
    /// <summary>
    /// Service for getting info about actual authorized user.
    /// </summary>
    public class HttpContextUser : IActiveUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="httpContextAccessor">Http context accessor.</param>
        public HttpContextUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = Check.NotNull(httpContextAccessor, nameof(httpContextAccessor));
        }

        /// <inheritdoc/>
        public int GetUserId() =>
            int.Parse(_httpContextAccessor.HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Sid).Value);
    }
}