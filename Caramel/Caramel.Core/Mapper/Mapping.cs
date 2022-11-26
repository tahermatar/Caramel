using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.DbModel.Models;
using Caramel.Models;
using Caramel.ModelViews.Blog;
using Caramel.ModelViews.Customer;
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

            CreateMap<Customer, CustomerLoginResponseViewModel>().ReverseMap();
            CreateMap<CustomerResult, Customer>().ReverseMap();
            CreateMap<CustomerModelViewModel, Customer>().ReverseMap();
            CreateMap<CustomerLoginResponseViewModel, Customer>().ReverseMap();
            CreateMap<CustomerUpdateModelView, Customer>().ReverseMap();
            CreateMap<PagedResult<CustomerResult>, PagedResult<Customer>>().ReverseMap();

            CreateMap<AddressResult, Address>().ReverseMap(); 
            CreateMap<Userpermissionview, Rolepermission>().ReverseMap();

            CreateMap<UserModelViewModel, User>().ReverseMap();







        }
    }
}
