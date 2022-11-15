using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.DbModel.Models;
using Caramel.Models;
using Caramel.ModelViews.Blog;
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

        }
    }
}
