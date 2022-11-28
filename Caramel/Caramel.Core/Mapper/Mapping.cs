using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.DbModel.Models;
using Caramel.Models;
using Caramel.ModelViews.Blog;
<<<<<<< HEAD
using Caramel.ModelViews.Order;
=======
using Caramel.ModelViews.Customer;
>>>>>>> development
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;

namespace CarProject.Mapper
{
    public class Mapping : Profile
    {
        
        public Mapping()
        {
            CreateMap<User, UserLoginResponseViewModel>().ReverseMap();
            CreateMap<User, UserModelViewModel>().ReverseMap();
            CreateMap<BlogViewModel, Blog>().ReverseMap();
            CreateMap<PagedResult<BlogViewModel>, PagedResult<Blog>>().ReverseMap();
            CreateMap<Resturant, ResturantLoginModelView>().ReverseMap();
            CreateMap<Resturant, ResturantLoginResponseModelView>().ReverseMap();
            CreateMap<Resturant, ResturantModelView>().ReverseMap();
            CreateMap<Resturant, ResturantRegisterViewModel>().ReverseMap();
<<<<<<< HEAD
            CreateMap<Order, OrderResult>().ReverseMap();
            CreateMap<Meal, MealRequest>().ReverseMap();
            CreateMap<Meal, MealModelView>().ReverseMap();
            CreateMap<MealCategory, MealCategoryModelView>().ReverseMap();
            CreateMap<MealCategory, CategoryRequest>().ReverseMap();
            CreateMap<Image, ImageModelView>().ReverseMap();
            CreateMap<MealCategory, ImageRequest>().ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryModelView>().ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryRequest>().ReverseMap();
=======

            CreateMap<Customer, CustomerLoginResponseViewModel>().ReverseMap();
            CreateMap<CustomerResult, Customer>().ReverseMap();
            CreateMap<CustomerModelViewModel, Customer>().ReverseMap();
            CreateMap<CustomerLoginResponseViewModel, Customer>().ReverseMap();
            CreateMap<CustomerUpdateModelView, Customer>().ReverseMap();
            CreateMap<PagedResult<CustomerResult>, PagedResult<Customer>>().ReverseMap();

            CreateMap<AddressResult, Address>().ReverseMap();


            CreateMap<ViewOrderViewModel, Order>().ReverseMap();
            CreateMap<CreateOrderViewModel, Order>().ReverseMap();
            CreateMap<ViewOrderViewModel, ShowOrdersViewModel>().ReverseMap();
            CreateMap<AddressResult, Address>().ReverseMap(); 
            CreateMap<Userpermissionview, Rolepermission>().ReverseMap();

            CreateMap<UserModelViewModel, User>().ReverseMap();
            CreateMap<UserModelViewModel, Resturant>().ReverseMap();



            



>>>>>>> development
        }
    }
}
