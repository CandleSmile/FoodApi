using BusinessLayer.Contracts;
using BusinessLayer.Services.Interfaces;
using FoodApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Utilities.ErrorHandle;

namespace FoodApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowFoodApp")]
    public class AuthController : Controller
    {
        private IUserService _userService;
        private IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("GetUsers")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegistrationModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Error((int)ErrorCodes.NoValidData, "Registration info is not valid"));
            }

            var existedUser = await _userService.GetUserByNameAsync(request.Username);
            if (existedUser != null)
            {
                return this.BadRequest(new Error((int)ErrorCodes.ObjectAlreadyExists, "The user exists."));
            }

            var user = new UserDto()
            {
                Username = request.Username,
                Password = request.Password
            };

            var savedUser = await _userService.AddAsync(user);

            return Ok(savedUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginModel request)
        {
            var userDto = await _userService.GetUserByNameAsync(request.Username);

            if (userDto == null)
            {
                return BadRequest(new Error((int)ErrorCodes.ObjectNotFound, "The User wasn't found"));
            }
            else
            {
                bool isValid = await _authService.VerifyPasswordHash(request.Password, userDto.Id);
                if (!isValid)
                {
                    return BadRequest(new Error((int)ErrorCodes.NoValidData, "Password was wrong"));
                }
                else
                {
                    string token = _authService.CreateAccessToken(userDto.Username);
                    string refreshToken = await _authService.CreateRefreshToken(userDto.Id);
                    _authService.SetTokensToCookies(token, refreshToken);
                    return Ok();
                }
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (await _authService.DeleteRefreshTokenFromDb())
            {
                _authService.DeleteTokensFromCookies();
                return Ok();
            }

            return BadRequest(new Error((int)ErrorCodes.NoValidData, "Problems with deleting refresh token"));
        }

       
        [HttpPost("CheckLogin")]
        public async Task<ActionResult<bool>> CheckLogin(string userName)
        {
            return await _authService.CheckIfLoginned(userName);
        }
    }
}
