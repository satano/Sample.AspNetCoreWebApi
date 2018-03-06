using Kros.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sample.AspNetCoreWebApi.Authorization;
using Sample.AspNetCoreWebApi.Filters;
using Sample.AspNetCoreWebApi.Models;
using Sample.AspNetCoreWebApi.ViewModels;

namespace Kros.OnlineMaterials.Server.Controllers
{
    [Route("token")]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {

        private readonly AuthenticationOption _params;
        private readonly IUserRepository _userRepository;

        public TokenController(IOptions<AuthenticationOption> optionsAccessor, IUserRepository userRepository)
        {
            _userRepository = Check.NotNull(userRepository, nameof(userRepository));
            _params = optionsAccessor.Value;
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public IActionResult Create([FromBody] UserViewModel user)
        {
            var model = _userRepository.GetByEmail(user.Email);

            if (model == null)
            {
                return Unauthorized();
            }

            var passord = new PasswordHasher(_params.Salt).EncryptPassword(user.Password);

            if (model.PasswordHash != passord)
            {
                return Unauthorized();
            }

            JwtTokenBuilder builder = new JwtTokenBuilder()
                .AddSecurityKey(JwtToken.GetSecret(_params.Key))
                .AddSubject(_params.Subject)
                .AddIssuer(_params.Issuer)
                .AddAudience(_params.Audience)
                .AddClaim(_params.UserClaimName, model.Id.ToString())
                .AddExpiry(_params.ExpirationTime);

            if (model.IsAdmin)
            {
                builder.AddClaim(_params.AdminClaimName, model.Id.ToString());
            }

            return Ok(builder.Build().Value);
        }
    }
}