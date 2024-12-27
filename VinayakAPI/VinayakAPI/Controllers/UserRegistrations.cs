using Microsoft.AspNetCore.Mvc;
using VinayakAPI.Interfaces;
using VinayakAPI.Models;

namespace VinayakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrations : Controller
    {
        private readonly ILogger<UserRegistrations> _logger;

        private readonly IUserRepository _userRepository;

        public UserRegistrations(ILogger<UserRegistrations> logger,IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;

        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserRegistration userRegistration)
        {
            _logger.LogTrace("CreateProduct started at {Time}", DateTime.Now);


            try
            {
                _logger.LogTrace("Processing in SomeMethod...");
                await _userRepository.AddUserRegistration(userRegistration);

                //  var dataToReturn = CreatedAtAction(nameof(userRegistration), new { id = userRegistration.Id }, userRegistration);

                if (userRegistration == null)
                {
                    return BadRequest("User with this email already exists.");
                }

                _logger.LogTrace("SomeMethod completed successfully at {Time}", DateTime.Now);


                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeMethod at {Time}", DateTime.Now);
                throw;
            }

        }
    }
}
