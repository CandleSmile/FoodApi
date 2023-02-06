using BusinessLayer.Dto;

namespace BusinessLayer.Services.Interfaces
{
    public interface IMealService
    {
        Task<IEnumerable<MealDto>> GetTop10MealsAsync();

        Task<IEnumerable<MealDto>> GetMeals(MealsFilterParams mealsFilterParams);

        Task<MealDto> GetMealById(int id);
    }
}
