using DataLayer.Items;

namespace DataLayer.Repositories.Interfaces
{
    public interface IIngredientRepository: IBaseRepository<Ingredient>
    {
        Task<Ingredient?> GetIngredientByNameAsync(string name);
    }
}
