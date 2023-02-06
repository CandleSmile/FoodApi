using BusinessLayer.Dto;

namespace BusinessLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

    }
}