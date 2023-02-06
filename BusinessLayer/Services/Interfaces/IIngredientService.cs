using BusinessLayer.Dto;

namespace BusinessLayer.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetIngredients();

    }
}
