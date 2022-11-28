using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews;
using Caramel.ModelViews.Order;
using Caramel.ModelViews.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.OrderManager
{
    public class OrderManager : IOrderManager
    {
        private readonly CaramelDbContext _context;
        private readonly IMapper _mapper;

        public OrderManager(CaramelDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ViewOrderViewModel> ViewMealOrder(UserModelViewModel currentUser)
        {
            var IsCustomer = _context.Customers.FirstOrDefault(x => x.Email == currentUser.Email);
            var IsRestorant = _context.Resturants.FirstOrDefault(x => x.Email == currentUser.Email);
            var User = _context.Users.FirstOrDefault(x => x.Email == currentUser.Email);
            
            List<ViewOrderViewModel> queryRes;

            if (IsCustomer != null)
            {
                queryRes = _context.ViewOrderViewModel.Where(x => x.CustomerId == IsCustomer.Id).ToList();
            }
            else
            if (IsRestorant != null)
            {
                queryRes = _context.ViewOrderViewModel.Where(x => x.RestorantId == IsRestorant.Id).ToList();
            }
            else if (true)
            {
                queryRes = _context.ViewOrderViewModel.Where(x => x.OrderId >= 1).ToList();
            }
            
            return queryRes;
        }

            public CreateOrderViewModel CreateMealOrder(UserModelViewModel currentUser, CreateOrderViewModel vm)
        {
            var Meal = _context.Meals.Where(x => x.Id == vm.MealId).FirstOrDefault();
            var TotalPrice = Meal.Price * vm.Quantity;
            var data = new Order
            {

                CustomerId = vm.CustomerId,
                Quantity = vm.Quantity,
                TotalPrice = TotalPrice,
                DateOfOrder = DateTime.Now,
                DateOfExecution = DateTime.Now.AddMinutes(30),
                MealId = vm.MealId,
                CreatedDate = DateTime.Now,
                CreatedBy = currentUser.Id,
                Archived = true,
                ResturantId = vm.ResturantId,
                Status = Convert.ToInt16(StatusEnum.InProgress)
            };

            _context.Orders.Add(data);
            _context.SaveChanges();

            return vm;


        }

        public void DeleteMealOrder(UserModelViewModel currentUser ,int id)
        {
           var order = _context.Orders.FirstOrDefault(x => x.Id == id)
                ?? throw new ServiceValidationException("Order not found");


            if (order != null)
            {
                order.Archived = false;
                order.UpdatedBy = currentUser.Id;
                order.UpdatedDate = DateTime.Now;

                _context.SaveChanges();
            }

        }

        public void FinishMealOrder(UserModelViewModel currentUser, int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id)
                ?? throw new ServiceValidationException("Order not found");

            if (order != null)
            {
                order.Status = Convert.ToInt16(StatusEnum.Completed);
                order.UpdatedBy = currentUser.Id;
                order.UpdatedDate = DateTime.Now;

                _context.SaveChanges();
            }
        }

    
    }
}