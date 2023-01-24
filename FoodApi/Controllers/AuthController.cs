using BusinessLayer.Contracts;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Helpers;

namespace FoodApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : Controller
    {
        private IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService= userService;
        }

        [HttpGet("GetUsers")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(UserDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new UserDto()
            {
                Username = request.Username,
                Password = request.Password
            };
            
            

            await _userService.AddAsync(user);

            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var userDto = await _userService.GetUserByNameAsync(request.Username);

            if (userDto == null)
            {
                return BadRequest("User not found.");
            }
            else
            {
                bool isValid = await _userService.VerifyPasswordHash(request.Password, userDto);
                if (!isValid)
                {
                    return BadRequest("Wrong Password.");
                }
                else
                {
                    string token = _userService.CreateToken(userDto);                    
                    return Ok(token);
                }
            }
        }
    }
}
