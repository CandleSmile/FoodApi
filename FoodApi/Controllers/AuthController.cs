using BusinessLayer.Dto;
using BusinessLayer.Dto;
using BusinessLayer.Services.Interfaces;
using FoodApi.Models;
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

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegistrationDto request)
        {
            var savedUser = await _authService.RegisterAsync(request);

            return Ok(savedUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginDto request)
        {
            await _authService.LoginUserAsync(request);

            return Ok();
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
