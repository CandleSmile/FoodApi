namespace BusinessLayer.Services.Implementation
{
    using AutoMapper;
    using BusinessLayer.Dto;
    using BusinessLayer.Services.Interfaces;
    using BusinessLayer.Validators;
    using DataLayer.Repositories.Interfaces;
    using FoodApi.Models;

    public class MealService : IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _imagePath;
        private readonly string _thumbnailPath;
        private readonly InputValidator _inputValidator = new InputValidator();
        /// <summary>
        /// Initializes a new instance of the <see cref="MealService"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public MealService(string imagePath, string thumbnailPath, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _imagePath = imagePath;
            _thumbnailPath = thumbnailPath;
        }

        public async Task<MealDto> GetMealById(int id)
        {
            var meal = await _unitOfWork.Meals.GetMealByIdWithInfoAsync(id);
            _inputValidator.ValidateIsNotNull(meal, "The meal wasn't found");

            var mealDto = _mapper.Map<MealDto>(meal);
            mealDto.Image = _thumbnailPath + mealDto.Image;
            return mealDto;
        }

        public async Task<IEnumerable<MealDto>> GetMeals(MealsFilterParams mealsFilterParams)
        {
            var meals = await _unitOfWork.Meals.GetMealsAsync(mealsFilterParams.SearchString, mealsFilterParams.CategoryId, mealsFilterParams.IngredientIds);
            var mealsDto = _mapper.Map<IEnumerable<MealDto>>(meals);
            foreach (var meal in mealsDto)
            {
                meal.Image = _imagePath + meal.Image;
            }

            return mealsDto;

        }

        public async Task<IEnumerable<MealDto>> GetTop10MealsAsync()
        {
            var mealsDto = _mapper.Map<IEnumerable<MealDto>>(await _unitOfWork.Meals.GetTop10MealsAsync());
            foreach (var meal in mealsDto)
            {
                meal.Image = _imagePath + meal.Image;
            }

            return mealsDto;
        }
    }
}
