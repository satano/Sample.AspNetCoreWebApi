namespace Sample.AspNetCoreWebApi.Models
{
    public interface IUserRepository
    {
         User GetByEmail(string email);
    }
}