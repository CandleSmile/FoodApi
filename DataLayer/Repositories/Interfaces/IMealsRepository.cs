using DataLayer.Items;

namespace DataLayer.Repositories.Interfaces
{
    public interface IMealRepository : IBaseRepository<Meal>
    {
        Task<Meal?> GetMealByNameAsync(string name);

        Task<IEnumerable<Meal>?> GetMealsAsync(int count, int skip);

        Task<IEnumerable<Meal>?> GetMealsAsync(string? searchString, int? idCategory, IEnumerable<int>? idsIngredients);

        Task<Meal?> GetMealByIdWithInfoAsync(int id);
    }
}
