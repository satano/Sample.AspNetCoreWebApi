namespace Sample.AspNetCoreWebApi.Models
{
    /// <summary>
    /// Interface, which describe repository for obtaining users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get user by email.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>User if exist; otherwise null.</returns>
         User GetByEmail(string email);
    }
}