using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Models;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Meal;
using Caramel.ModelViews.Rate;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;

namespace CarProject.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserLoginResponseViewModel>().ReverseMap();
            CreateMap<Resturant, ResturantLoginModelView>().ReverseMap();
            CreateMap<Resturant, ResturantLoginResponseModelView>().ReverseMap();
            CreateMap<Resturant, ResturantModelView>().ReverseMap();
            CreateMap<Resturant, ResturantRegViewModel>().ReverseMap();
            CreateMap<Resturant, ResturantViewAllModelView>().ReverseMap();
            CreateMap<Resturant, ResturantRegisterViewModel>().ReverseMap();
            CreateMap<Resturant, ResturantUpdateModelView>().ReverseMap();
            CreateMap<Resturant, ResturantResponse>().ReverseMap();
            CreateMap<PagedResult<ResturantModelView>, PagedResult<Resturant>>().ReverseMap();

            CreateMap<Customer, CustomerLoginResponseViewModel>().ReverseMap();
            CreateMap<CustomerResult, Customer>().ReverseMap();
            CreateMap<CustomerModelViewModel, Customer>().ReverseMap();
            CreateMap<CustomerLoginResponseViewModel, Customer>().ReverseMap();
            CreateMap<CustomerUpdateModelView, Customer>().ReverseMap();
            CreateMap<PagedResult<CustomerResult>, PagedResult<Customer>>().ReverseMap();
            
            CreateMap<AddressResult, Address>().ReverseMap();
            CreateMap<ResturantViewAllModelView, Address>().ReverseMap();
            CreateMap<Userpermissionview, Rolepermission>().ReverseMap();

            CreateMap<UserModelViewModel, User>().ReverseMap();
            CreateMap<UserModelViewModel, Resturant>().ReverseMap();

            //CreateMap<CreateOrderViewModel, Order>().ReverseMap();

            CreateMap<MealCreateModelView, Meal>().ReverseMap();
            CreateMap<MealResult, Meal>().ReverseMap();
            CreateMap<PagedResult<MealResult>, PagedResult<Meal>>().ReverseMap();
            CreateMap<MealModelView, Meal>().ReverseMap();

            CreateMap<UserModelViewModel, Rate>().ReverseMap();
            CreateMap<AddRateModelView, Rate>().ReverseMap();
            CreateMap<RateResponse, Rate>().ReverseMap();
            CreateMap<RateResult, Rate>().ReverseMap();
            CreateMap<PagedResult<RateResult>, PagedResult<Rate>>().ReverseMap();
        }
    }
}
