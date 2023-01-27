using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Contracts.DBLoad;
using DataLayer.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Meal, MealDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Area, AreaDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Meal, MealDb>().ReverseMap();
            CreateMap<Category, CategoryDb>().ReverseMap();
            CreateMap<Area, AreaDb>().ReverseMap();
            CreateMap<Ingredient, IngredientDb>().ReverseMap();
        }
    }
}
