namespace FoodApi.Controllers
{
    using BusinessLayer.Dto;
    using BusinessLayer.Services.Interfaces;
    using FoodApi.Models;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowFoodApp")]
    public class MealsController : ControllerBase
    {
        private readonly IMealService mealService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MealsController"/> class.
        /// </summary>
        /// <param name="mealService"></param>
        public MealsController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        [HttpPost("GetMeals")]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetMeals(MealsFilterParams mealsFilterParams)
        {
            var meals = await this.mealService.GetMeals(mealsFilterParams);
            return Ok(meals);
        }

        [HttpGet("GetLatestMeals")]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetLatestMeals()
        {
            var meals = await this.mealService.GetTop10MealsAsync();

            return Ok(meals);
        }

        [HttpGet("GetMeal")]
        public async Task<ActionResult<MealDto>> GetLMeal(int id)
        {
            var meal = await this.mealService.GetMealById(id);
            return Ok(meal);
        }
    }
}
