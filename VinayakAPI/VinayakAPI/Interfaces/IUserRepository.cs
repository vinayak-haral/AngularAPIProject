using VinayakAPI.Models;
using VinayakAPI.Repository;

namespace VinayakAPI.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserRegistration(UserRegistration userRegistration);
        //Task AddProduct(Product product);
    }
}
