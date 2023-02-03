using BusinessLayer.Dto;
using System.Security.Claims;

namespace BusinessLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

    }
}
