namespace Sample.AspNetCoreWebApi.Services
{
    /// <summary>
    /// Interface, which describe service for getting info about actual authorized user.
    /// </summary>
    public interface IActiveUser
    {
        /// <summary>
        /// Get actual authorized user id.
        /// </summary>
        /// <returns>Authorized user id.</returns>
         int GetUserId();
    }
}