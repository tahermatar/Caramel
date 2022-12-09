using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews.User;
using System.Collections.Generic;
using System.Linq;

namespace Caramel.Core.Mangers.CommonManger
{
    public class CommonManager : ICommonManager
    {
        private readonly IMapper _mapper;
        private readonly CaramelDbContext _context;

        public CommonManager(CaramelDbContext context , IMapper mapper)
        {
            _mapper = mapper;
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

        public UserModelViewModel GetResturanRole(UserModelViewModel resturant)
        {
            var dbresturant = _context.Resturants.FirstOrDefault(x => x.Id == resturant.Id)
                ?? throw new ServiceValidationException("User Is not valid");


            var mappedUser = new UserModelViewModel
            {
                Id = dbresturant.Id,
                UserName = dbresturant.UserName,
                Email = dbresturant.Email,
            };
            //mappedUser.Permissions = _context.Userpermissionviews.Where(x => x.UserId == resturant.Id).ToList();
            mappedUser.Permissions = _mapper.Map<List<Userpermissionview>>(_context.Rolepermissions.Where(x => x.RoleId == 4).ToList());

            return _mapper.Map<UserModelViewModel>(dbresturant);
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

            mappedUser.Permissions = _mapper.Map<List<Userpermissionview>>(_context.Rolepermissions.Where(x => x.RoleId == 1).ToList());
            // mappedUser.Permissions = _context.Userpermissionviews.Where(x => x.UserId == customer.Id).ToList();

            return mappedUser;
        }


    }
}
