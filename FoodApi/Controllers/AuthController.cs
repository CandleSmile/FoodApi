using BusinessLayer.Dto;
using BusinessLayer.Services.Interfaces;
using FoodApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utilities.ErrorHandle;

namespace FoodApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowFoodApp")]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegistrationDto request)
        {
            var savedUser = await _authService.RegisterAsync(request);
            return Ok(savedUser);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("Id").Value;
            await _authService.ChangePasswordAsync(request, Convert.ToInt32(userId));
            return Ok();
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
