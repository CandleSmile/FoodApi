using BusinessLayer;
using BusinessLayer.Contracts;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Utilities.Helpers;

namespace FoodApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowFoodApp")]
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
                return BadRequest(new Error((int)ErrorCodes.ObjectNotFound, "The User wasn't found"));
            }
            else
            {
                bool isValid = await _userService.VerifyPasswordHash(request.Password, userDto);
                if (!isValid)
                {
                    return BadRequest(new Error((int)ErrorCodes.NoValidData, "Password was wrong"));                    
                }
                else
                {
                    string token = _userService.CreateToken(userDto.Username);                

                    return Ok(new
                    {
                        token,
                        refreshToken =  await _userService.CreateRefreshToken(userDto)
                    });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Refresh(string token, string refreshToken)
        {
           (var newJwtToken, var newRefreshToken) = await  _userService.RefreshToken(token, refreshToken);

            return  Ok(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }
    }
}
