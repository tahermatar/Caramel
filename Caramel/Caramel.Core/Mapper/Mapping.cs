using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.DbModel.Models;
using Caramel.Models;
using Caramel.ModelViews.Blog;
using Caramel.ModelViews.Order;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;

namespace CarProject.Mapper
{
    public class Mapping : Profile
    {
        
        public Mapping()
        {
            CreateMap<User, UserLoginResponseViewModel>().ReverseMap();
            CreateMap<BlogViewModel, Blog>().ReverseMap();
            CreateMap<PagedResult<BlogViewModel>, PagedResult<Blog>>().ReverseMap();
            CreateMap<Resturant, ResturantLoginModelView>().ReverseMap();
            CreateMap<Resturant, ResturantLoginResponseModelView>().ReverseMap();
            CreateMap<Resturant, ResturantModelView>().ReverseMap();
            CreateMap<Resturant, ResturantRegisterViewModel>().ReverseMap();
            CreateMap<Order, OrderResult>().ReverseMap();
            CreateMap<Meal, MealRequest>().ReverseMap();
            CreateMap<Meal, MealModelView>().ReverseMap();
            CreateMap<MealCategory, MealCategoryModelView>().ReverseMap();
            CreateMap<MealCategory, CategoryRequest>().ReverseMap();
            CreateMap<Image, ImageModelView>().ReverseMap();
            CreateMap<MealCategory, ImageRequest>().ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryModelView>().ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryRequest>().ReverseMap();
        }
    }
}
