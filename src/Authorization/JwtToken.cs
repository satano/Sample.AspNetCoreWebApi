using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Sample.AspNetCoreWebApi.Authorization
{
    /// <summary>
    /// Serialization of SecurityToken.
    /// </summary>
    internal sealed class JwtToken
    {

        #region Constants

        public const string ADMIN_NAME = "Admin";
        public const string USER_NAME = "User";

        # endregion

        private JwtSecurityToken _token;

        internal JwtToken(JwtSecurityToken token)
        {
            _token = token;
        }

        /// <summary>
        /// Token serialization.
        /// </summary>
        /// <returns>Serializovaný token.</returns>
        public string Value => new JwtSecurityTokenHandler().WriteToken(_token);

        /// <summary>
        /// Encrypts phrase with symetric key.
        /// </summary>
        /// <param name="phrase">Phrase to encrypt.</param>
        /// <returns>Encrypted phrase.</returns>
        public static SymmetricSecurityKey GetSecret(string phrase)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(phrase));
        }
    }
}