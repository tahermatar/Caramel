using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews;
using Caramel.ModelViews.Order;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public CreateOrderViewModel CreateMealOrder(UserModelViewModel currentUser, CreateOrderViewModel vm)
        {
            var checkCustomer = _context.Customers.FirstOrDefault(x => x.Id == currentUser.Id)
                                ?? throw new ServiceValidationException("Order just for customer");

            var checkMealExist = _context.Meals.FirstOrDefault(x => x.Id == vm.MealId)
                                 ?? throw new ServiceValidationException("Meal not Exist");

            var checkResturuntExist = _context.Resturants.FirstOrDefault(x => x.Id == vm.ResturantId)
                                      ?? throw new ServiceValidationException("Resturaant not Exist");

            var Meal = _context.Meals.Where(x => x.Id == vm.MealId).FirstOrDefault();

            float TotalPrice = (float)(Meal.Price * vm.Quantity);

            var data = new Order
            {
                CustomerId = currentUser.Id,
                Quantity = vm.Quantity,
                TotalPrice = TotalPrice,
                DateOfOrder = DateTime.Now,
                DateOfExecution = DateTime.Now.AddMinutes(30),
                MealId = vm.MealId,
                CreatedDate = DateTime.Now,
                CreatedBy = currentUser.Id,
                Archived = false,
                ResturantId = vm.ResturantId,
                Status = Convert.ToInt16(StatusEnum.InProgress)
            };

            _context.Orders.Add(data);
            _context.SaveChanges();

            return vm;
        }

        public List<ViewOrderViewModel> ViewMealOrder(UserModelViewModel currentUser)
        {
            var IsCustomer = _context.Customers.FirstOrDefault(x => x.Email == currentUser.Email);

            var IsResturant = _context.Resturants.FirstOrDefault(x => x.Email == currentUser.Email);

            var User = _context.Users.FirstOrDefault(x => x.Email == currentUser.Email);

            //var OrderInfo = _context.Orders.Include(x => x.Resturants).ToList();

            //List<OrderInfoViewModel> listOrderInfo = new List<OrderInfoViewModel>();
            
            List<ViewOrderViewModel> queryRes;

            if (IsCustomer != null)
            {
                queryRes = _context.ViewOrderViewModel.Where(x => x.CustomerId == IsCustomer.Id).ToList();
            }

            else if (IsResturant != null)
            {
                queryRes = _context.ViewOrderViewModel.Where(x => x.RestorantId == IsResturant.Id).ToList();
            }

            else if (true)
            {
                queryRes = _context.ViewOrderViewModel.Where(x => x.OrderId >= 1).ToList();
            }
            
            return queryRes;
        }

        public void FinishMealOrder(UserModelViewModel currentUser, int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id)
                ?? throw new ServiceValidationException("Order not found");

            if (order != null)
            {
                order.Status = Convert.ToInt16(StatusEnum.Completed);
                order.UpdatedBy = currentUser.Id;
                order.Archived = true;
                order.UpdatedDate = DateTime.Now;

                _context.SaveChanges();
            }
        }

        public void DeleteMealOrder(UserModelViewModel currentUser ,int id)
        {
           var order = _context.Orders.FirstOrDefault(x => x.Id == id)
                       ?? throw new ServiceValidationException("Order not found");

            if (order != null)
            {
                order.Archived = true;
                order.UpdatedBy = currentUser.Id;
                order.UpdatedDate = DateTime.Now;

                _context.SaveChanges();
            }

        }
    }
}