using Kros.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Sample.AspNetCoreWebApi.Authorization
{
    /// <summary>
    /// Building of JwtToken.
    /// </summary>
    internal sealed class JwtTokenBuilder
    {
        private SecurityKey _securityKey = null;
        private string _subject = "";
        private string _issuer = "";
        private string _audience = "";
        private Dictionary<string, string> _claims = new Dictionary<string, string>();
        private int _expireInMinutes = 5;

        /// <summary>
        /// Sets security key.
        /// </summary>
        /// <param name="securityKey">New key.</param>
        /// <returns>This instance.</returns>
        internal JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            _securityKey = securityKey;
            return this;
        }

        /// <summary>
        /// Sets subject.
        /// </summary>
        /// <param name="subject">New value.</param>
        /// <returns>This instance.</returns>
        internal JwtTokenBuilder AddSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        /// <summary>
        /// Sets issuer.
        /// </summary>
        /// <param name="issuer">New value.</param>
        /// <returns>This instance.</returns>
        internal JwtTokenBuilder AddIssuer(string issuer)
        {
            _issuer = issuer;
            return this;
        }

        /// <summary>
        /// Sets audience.
        /// </summary>
        /// <param name="audience">New value.</param>
        /// <returns>This instance.</returns>
        internal JwtTokenBuilder AddAudience(string audience)
        {
            _audience = audience;
            return this;
        }

        /// <summary>
        /// Appends next claim.
        /// </summary>
        /// <param name="type">Type of claim.</param>
        /// <param name="value">Value of claim.</param>
        /// <returns>This instance.</returns>
        internal JwtTokenBuilder AddClaim(string type, string value)
        {
            _claims.Add(type, value);
            return this;
        }

        /// <summary>
        /// Sets expire time in minutes.
        /// </summary>
        /// <param name="expiryInMinutes">New expire time in minutes.</param>
        /// <returns>This instance.</returns>
        internal JwtTokenBuilder AddExpiry(int expiryInMinutes)
        {
            _expireInMinutes = expiryInMinutes;
            return this;
        }

        /// <summary>
        /// Builds new instance of JwtToken.
        /// </summary>
        /// <param name="expiryInMinutes">New expire time in minutes.</param>
        /// <returns>This instance.</returns>
        internal JwtToken Build()
        {
            CheckArguments();

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, _subject),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(_claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                              issuer: _issuer,
                              audience: _audience,
                              claims: claims,
                              expires: DateTime.UtcNow.AddMinutes(_expireInMinutes),
                              signingCredentials: new SigningCredentials(
                                     _securityKey,
                                     SecurityAlgorithms.HmacSha256));
            return new JwtToken(token);
        }

        #region "Helpers"

        private void CheckArguments()
        {
            Check.NotNull(_securityKey, nameof(_securityKey));
            Check.NotNullOrEmpty(_subject, nameof(_subject));
            Check.NotNullOrEmpty(_issuer, nameof(_issuer));
            Check.NotNullOrEmpty(_audience, nameof(_audience));
        }

        #endregion
    }
}
