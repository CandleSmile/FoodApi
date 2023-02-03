namespace FoodApi.Controllers
{
    using BusinessLayer.Dto;
    using BusinessLayer.Services.Interfaces;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowFoodApp")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService ingredientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="IngredientsController"/> class.
        /// </summary> 
        /// <param name="ingredientService"></param>
        public IngredientsController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }


        [HttpGet("GetIngredients")]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetIngredients()
        {
            return Ok(await ingredientService.GetIngredients());
        }
    }
}
