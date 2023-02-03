using AutoMapper;
using BusinessLayer.Dto;
using BusinessLayer.Dto.DBLoad;
using BusinessLayer.DTto;
using DataLayer.Items;

namespace BusinessLayer
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegistrationDto, User>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Meal, MealDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Area, AreaDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            //start to delete
            CreateMap<Meal, MealDb>().ReverseMap();
            CreateMap<Category, CategoryDb>().ReverseMap();
            CreateMap<Area, AreaDb>().ReverseMap();
            CreateMap<Ingredient, IngredientDb>().ReverseMap();
            //end to delete
        }
    }
}
