namespace FoodApi.Controllers
{
    using BusinessLayer.Contracts;
    using BusinessLayer.Services.Implementation;
    using BusinessLayer.Services.Interfaces;
    using FoodApi.Models;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowFoodApp")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="mealService"></param>
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await categoryService.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
