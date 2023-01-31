namespace BusinessLayer.Services.Implementation
{
    using AutoMapper;
    using BusinessLayer.Contracts;
    using BusinessLayer.Services.Interfaces;
    using DataLayer.Repositories.Interfaces;
    using Utilities.ErrorHandle;

    public class MealService : IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MealService"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public MealService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MealDto> GetMealById(int id)
        {
            var meal = await _unitOfWork.Meals.GetMealByIdWithInfoAsync(id);
            if (meal == null)
            {
                throw new ModelNotFoundException("Meal", "$id {id}");
            }
            return _mapper.Map<MealDto>(meal);
        }

        public async Task<IEnumerable<MealDto>> GetMeals(string? searchString, int? idCategory, IEnumerable<int>? idsIngredients)
        {

            return _mapper.Map<IEnumerable<MealDto>>(await _unitOfWork.Meals.GetMealsAsync(searchString, idCategory, idsIngredients));

        }

        public async Task<IEnumerable<MealDto>> GetTop10MealsAsync()
        {
            return _mapper.Map<IEnumerable<MealDto>>(await _unitOfWork.Meals.GetTop10MealsAsync());
        }
    }
}
