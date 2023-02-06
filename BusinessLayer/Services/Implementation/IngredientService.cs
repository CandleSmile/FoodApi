using AutoMapper;
using BusinessLayer.Dto;
using BusinessLayer.Services.Interfaces;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.Implementation
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IngredientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> GetIngredients()
        {
            var result = await _unitOfWork.Ingredients.GetAllAsync();
            return _mapper.Map<IEnumerable<IngredientDto>>(result);
        }
    }
}
