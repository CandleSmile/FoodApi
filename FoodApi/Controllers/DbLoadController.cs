using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Contracts.DBLoad;
using BusinessLayer.Services.Implementation;
using BusinessLayer.Services.Interfaces;
using FoodApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Utilities.ErrorHandle;
using Utilities.Helpers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;


//to delete
namespace FoodApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowFoodApp")]
    public class DbLoadController : Controller
    {
        private IDbLoaderService _dbLoaderService;

        public DbLoadController(IDbLoaderService dbLoaderService)
        {
            _dbLoaderService = dbLoaderService;
        }

        [HttpPost("LoadDb")]
        public async Task<ActionResult<string>> LoadDb(DbLoadModel model) {
            if (ModelState.IsValid)
            {
                return await _dbLoaderService.LoadDb(model);
            }
            else
            {
                return BadRequest(new Error((int)ErrorCodes.NoValidData, "Not valid data"));
            }
        }
    }
}
