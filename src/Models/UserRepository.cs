using System.Collections.Generic;

namespace Sample.AspNetCoreWebApi.Models
{
    public class UserRepository : IUserRepository
    {

        private Dictionary<string, User> _users = new Dictionary<string, User>();

        public UserRepository()
        {
            _users["gilbert.johnston@example.com"] = new User()
            {
                Id = 1, Email = "gilbert.johnston@example.com", PasswordHash = "UMMZqDlyjpmLjSxiB4vJBVh8acSbd1+euXakYdytdNI="
            };
            _users["noa.leroy@example.com"] = new User()
            {
                Id = 2,
                Email = "noa.leroy@example.com", PasswordHash = "UMMZqDlyjpmLjSxiB4vJBVh8acSbd1+euXakYdytdNI="
            };
            _users["brooklyn.beijersbergen@example.com"] = new User()
            {
                Id = 3,
                Email = "brooklyn.beijersbergen@example.com", PasswordHash = "UMMZqDlyjpmLjSxiB4vJBVh8acSbd1+euXakYdytdNI="
            };
            _users["iam.olivier@example.com"] = new User()
            {
                Id = 4,
                Email = "iam.olivier@example.com", PasswordHash = "UMMZqDlyjpmLjSxiB4vJBVh8acSbd1+euXakYdytdNI="
            };
            _users["karla.kristensen@example.com"] = new User()
            {
                Id = 5,
                Email = "karla.kristensen@example.com", PasswordHash = "UMMZqDlyjpmLjSxiB4vJBVh8acSbd1+euXakYdytdNI="
            };
            _users["janko.hrasko@example.com"] = new User()
            {
                Id = 6,
                Email = "janko.hrasko@example.com", PasswordHash = "UMMZqDlyjpmLjSxiB4vJBVh8acSbd1+euXakYdytdNI="
            };
            _users["admin@example.com"] = new User()
            {
                Id = 7,
                Email = "admin@example.com", PasswordHash = "UMMZqDlyjpmLjSxiB4vJBVh8acSbd1+euXakYdytdNI=",
                IsAdmin = true
            };
        }

        /// <inheritdoc/>
        public User GetByEmail(string email) => _users[email];
    }
}