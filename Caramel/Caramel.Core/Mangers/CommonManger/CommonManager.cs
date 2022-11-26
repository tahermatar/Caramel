using AutoMapper;
using Caramel.Common.Exceptions;
using Caramel.Common.Extinsions;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews.Blog;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.CommonManger
{
    public class CommonManager : ICommonManager
    {
        private readonly IMapper _mapper;
        private readonly IBlogManager _blogManager;
        private readonly CaramelDbContext _context;

        public CommonManager(CaramelDbContext context , IMapper mapper , IBlogManager blogManager)
        {
            _mapper = mapper;
            _blogManager = blogManager;
            _context = context;
        }
        public UserModelViewModel GetUserRole(UserModelViewModel user)
        {
            var dbuser = _context.Users.FirstOrDefault(x => x.Id == user.Id)
                ?? throw new ServiceValidationException("User Is not valid");

            var mappedUser = new UserModelViewModel
            {
                Id = dbuser.Id,
                UserName = dbuser.UserName,
                Email = dbuser.Email,
            };
            mappedUser.Permissions = _context.Userpermissionviews.Where(x => x.UserId == user.Id).ToList();

            return _mapper.Map<UserModelViewModel>(dbuser);
        }

        //public UserModelViewModel GetUserBlogs(UserModelViewModel user)
        //{
        //    var res = _blogManager.GetBlog(user, 1);
        //    return res;
        //}

        public ResturantModelView GetResturanRole(ResturantModelView resturant)
        {
            var dbresturant = _context.Resturants.FirstOrDefault(x => x.Id == resturant.Id)
                ?? throw new ServiceValidationException("User Is not valid");

            var mappedUser = new UserModelViewModel
            {
                Id = dbresturant.Id,
                UserName = dbresturant.UserName,
                Email = dbresturant.Email,
            };
            mappedUser.Permissions = _context.Userpermissionviews.Where(x => x.UserId == resturant.Id).ToList();

            return _mapper.Map<ResturantModelView>(dbresturant);
        }

        public CustomerModelViewModel GetCustomerRole(CustomerModelViewModel customer)
        {
            var dbcustomer = _context.Customers.FirstOrDefault(x => x.Id == customer.Id)
                ?? throw new ServiceValidationException("User Is not valid");

            var mappedUser = new CustomerModelViewModel
            {
                Id = dbcustomer.Id,
                UserName = dbcustomer.UserName,
                Email = dbcustomer.Email,
            };
           // mappedUser.Permissions = _context.Userpermissionviews.Where(x => x.UserId == customer.Id).ToList();

            return _mapper.Map<CustomerModelViewModel>(dbcustomer);
        }


        public UserModelViewModel GetCustomerRole(UserModelViewModel customer)
        {
            var dbcustomer = _context.Customers.FirstOrDefault(x => x.Id == customer.Id)
                ?? throw new ServiceValidationException("User Is not valid");

            var mappedUser = new UserModelViewModel
            {
                Id = dbcustomer.Id,
                UserName = dbcustomer.UserName,
                Email = dbcustomer.Email,
            };

            mappedUser.Permissions = _mapper.Map<List<Userpermissionview>>(_context.Rolepermissions.Where(x => x.RoleId == 3).ToList());
            // mappedUser.Permissions = _context.Userpermissionviews.Where(x => x.UserId == customer.Id).ToList();

            return mappedUser;
        }


    }
}
