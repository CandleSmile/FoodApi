namespace BusinessLayer.Services.Implementation
{
    using AutoMapper;
    using BusinessLayer.Contracts;
    using BusinessLayer.Contracts.DBLoad;
    using BusinessLayer.Services.Interfaces;
    using DataLayer.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Configuration;
    using Utilities.ErrorHandle;
    using static Google.Apis.Drive.v3.Data.File.ContentHintsData;

    public class MealService : IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _imagePath;
        private readonly string _thumbnailPath;
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
            if (meal == null)
            {
                throw new ModelNotFoundException("Meal", "$id {id}");
            }

            var mealDto = _mapper.Map<MealDto>(meal);
            mealDto.Image = _thumbnailPath + mealDto.Image;
            return mealDto;
        }

        public async Task<IEnumerable<MealDto>> GetMeals(string? searchString, int? idCategory, IEnumerable<int>? idsIngredients)
        {
            var meals = await _unitOfWork.Meals.GetMealsAsync(searchString, idCategory, idsIngredients);
            var mealsDto = _mapper.Map<IEnumerable<MealDto>>(await _unitOfWork.Meals.GetMealsAsync(searchString, idCategory, idsIngredients));
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
