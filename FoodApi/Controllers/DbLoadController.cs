using BusinessLayer.Dto.DBLoad;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Utilities.ErrorHandle;


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
        public async Task<ActionResult<string>> LoadDb(DbLoadModel model)
        {
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
