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
        
        public CategoryService(IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;            
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.Categories.GetAllAsync());
        }
    }
}
