using VinayakAPI.Models;

namespace VinayakAPI.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserRegistration(UserRegistration user);
    }
}
