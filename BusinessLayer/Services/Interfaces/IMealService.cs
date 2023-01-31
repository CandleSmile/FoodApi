using BusinessLayer.Contracts;
using System.Security.Claims;

namespace BusinessLayer.Services.Interfaces
{
    public interface IMealService
    {
        Task<IEnumerable<MealDto>> GetTop10MealsAsync();
        Task<IEnumerable<MealDto>> GetMeals(string? searchString, int? idCategory, IEnumerable<int>? idsIngredients);

        Task<MealDto> GetMealById(int id);
       
    }
}
