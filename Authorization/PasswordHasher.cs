using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace Sample.AspNetCoreWebApi.Authorization
{
    internal class PasswordHasher
    {
        private readonly string _salt;

        public PasswordHasher(string salt)
        {
            _salt = salt;
        }

        public string EncryptPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(_salt);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 11122,
                numBytesRequested: 256 / 8));
        }
    }
}