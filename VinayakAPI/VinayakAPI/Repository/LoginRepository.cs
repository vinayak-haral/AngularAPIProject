using Microsoft.AspNetCore.Mvc;
using VinayakAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VinayakAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using VinayakAPI.Data;

namespace VinayakAPI.Repository
{
    public class LoginRepository:ILogin
    {
        private readonly mainAPIDbContext _context;

        public LoginRepository(mainAPIDbContext context)
        {
            _context = context;
        }

        // Simulating database user validation for demo purposes
        public async Task<LoginModel> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
             //await _context.LoginModel
             //   .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            var dataToReturn = await _context.GetAllLogin();

             return  dataToReturn.FirstOrDefault(u => u.Username == username && u.Password == password);

           // return null;
        }

    }
}
