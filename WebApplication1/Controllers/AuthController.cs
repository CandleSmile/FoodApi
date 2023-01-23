using BusinessLayer.Services.Interfaces;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace FoodApi.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService= userService;
        }
        [HttpGet("getUsers")]
        public JsonResult GetUsers()
        {
            return Json(_userService.GetUsers());
        }
    }
}
