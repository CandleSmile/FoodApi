using BusinessLayer.Dto;

namespace BusinessLayer.Services.Interfaces
{
    public interface IMealService
    {
        Task<IEnumerable<MealDto>> GetMealsAsync(int count, int skip);

        Task<IEnumerable<MealDto>> GetMeals(MealsFilterParams mealsFilterParams);

        Task<MealDto> GetMealById(int id);
    }
}
