using BusinessLayer.Dto;
using System.Security.Claims;

namespace BusinessLayer.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetIngredients();      
       
    }
}
