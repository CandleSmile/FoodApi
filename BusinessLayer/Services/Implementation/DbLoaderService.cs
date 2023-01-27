using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Contracts.DBLoad;
using BusinessLayer.Services.Interfaces;
using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementation
{
    public class DbLoaderService: IDbLoaderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DbLoaderService(IUnitOfWork unitOfWork, IMapper mapper) { 
            _unitOfWork= unitOfWork;
            _mapper= mapper;
        }

        public async Task<string> LoadDb(DbLoadModel model)
        {
            
            try {

                await _unitOfWork.Categories.AddRangeAsync(_mapper.Map<List<Category>>(model.Categories));
                await _unitOfWork.Areas.AddRangeAsync(_mapper.Map<List<Area>>(model.Areas));
                await _unitOfWork.Ingredients.AddRangeAsync(_mapper.Map<List<Ingredient>>(model.Ingredients));
                //save tags
                List<Tag> tags = new List<Tag>();
                model.Meals.ForEach(meal =>
                {
                    if (meal.TagStrings != null)
                        meal.TagStrings.ForEach(tag =>
                        {
                            if (tags.Find(t => t.Name == tag) == null)
                            {
                                tags.Add(new Tag() { Name = tag });
                            }
                        });
                });
                await _unitOfWork.Tags.AddRangeAsync(tags);
                await _unitOfWork.SaveAsync();

                //save meals with cat, ingredients, tags
                var meals = new List<Meal>();
                foreach (var mealDb in model.Meals)
               
                {
                    var meal = _mapper.Map<Meal>(mealDb);
                    meal.Category = await _unitOfWork.Categories.GetCategoryByNameAsync(mealDb.CategoryString);
                    meal.Area = await _unitOfWork.Areas.GetAreaByNameAsync(mealDb.AreaString);

                    meal.Ingredients = new List<Ingredient>();
                    foreach(var ing in mealDb.IngredientsStrings)
                   
                    {
                        var findIgr = await _unitOfWork.Ingredients.GetIngredientByNameAsync(ing);
                        if (findIgr != null)
                        {
                            meal.Ingredients.Add(findIgr);
                        }
                    };

                    meal.Tags = new List<Tag>();
                    if (mealDb.TagStrings != null)
                    {
                        foreach(var tag in mealDb.TagStrings)
                      
                        {
                            var findTag = await _unitOfWork.Tags.GetTagByNameAsync(tag);
                            if (findTag != null)
                            {
                                meal.Tags.Add(findTag);
                            }
                        };
                    }
                    meals.Add(meal);
                };

                await _unitOfWork.Meals.AddRangeAsync(meals);
                await _unitOfWork.SaveAsync();
                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
      
    }
}
