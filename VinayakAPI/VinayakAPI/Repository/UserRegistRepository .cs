using VinayakAPI.Data;
using VinayakAPI.Interfaces;
using VinayakAPI.Models;

namespace VinayakAPI.Repository
{
    public class UserRegistRepository: IUserRepository
    {
        // Instance of DbContex Class
        private readonly mainAPIDbContext _context;

        public UserRegistRepository(mainAPIDbContext context)
        {
            _context = context;
        }
              
        public async Task AddUserRegistration(UserRegistration user)
        {
           await _context.InsertUserRegisterAsync(user);
        }
    }
}
