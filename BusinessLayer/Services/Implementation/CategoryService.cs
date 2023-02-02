using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Services.Interfaces;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _imagePath;

        public CategoryService(string imagePath, IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imagePath = imagePath;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.Categories.GetAllAsync());
            foreach (var category in categoriesDto)
            {
                category.Image = _imagePath + category.Image;
            }

            return categoriesDto;
        }
    }
}
