using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VinayakAPI.Interfaces;
using VinayakAPI.Models;
using VinayakAPI.Service;

namespace VinayakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly ILogin _login;

        public AuthController(IConfiguration configuration,TokenService tokenService,ILogin login)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _login = login;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // For demo, validate username/password
            //if (loginModel.Username != "testuser" || loginModel.Password != "password")
            //    return Unauthorized();

            // Generate JWT token for the user
            //var token = _tokenService.GenerateJwtToken(loginModel);
            // Return the token in the response
            //return Ok(new { token });

            // Validate the username and password using the repository
            var user = await _login.GetUserByUsernameAndPasswordAsync(loginModel.Username, loginModel.Password);

            if(user == null)
            {
                // Return 401 if the user credentials are incorrect
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var token = _tokenService.GenerateJwtToken(user);

            // Return the token in the response
            return Ok(new { token });
        }

        ////
        //private string GenerateJwtToken(string username)
        //{
        //    var jwtSettings = _configuration.GetSection("JwtSettings");
        //    var secretKey = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("SecretKey"));
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //        new Claim(ClaimTypes.Name, username)
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        Issuer = jwtSettings.GetValue<string>("Issuer"),
        //        Audience = jwtSettings.GetValue<string>("Audience"),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

    }
}
