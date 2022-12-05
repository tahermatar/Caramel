using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews.Rate;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.RateManager.cs
{
    public class RateManager : IRateManager
    {
        private readonly CaramelDbContext _context;
        private readonly IMapper _mapper;
        public RateManager(CaramelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void DeleteRate(UserModelViewModel currentUser, int id)
        {
            var checkCoustomer = _context.Customers.FirstOrDefault(x => x.Id == currentUser.Id)
                                              ?? throw new ServiceValidationException("You have no access to delete this rating");

            var checkRate = _context.Rates.FirstOrDefault(x => x.Id == id)
                                        ?? throw new ServiceValidationException("The rating id does not exist");

            if (checkRate.CustomerId == currentUser.Id && !currentUser.IsSuperAdmin)
            {
                checkRate.Archived = true;
                _context.SaveChanges();
            }
            else
            {
                throw new ServiceValidationException(300,"You have no access to delete this rating Or " +
                                                         "rating id does not exist in customer");
            }

        }

        public AddRateModelView PutRate(UserModelViewModel currentUser, AddRateModelView addRate)
        {

            var order = _context.Orders
                                .FirstOrDefault(x => x.CustomerId == currentUser.Id)
                                ?? throw new ServiceValidationException("you have no access to rate this restaurant");

            var resturant = new Resturant();

            var res = _context.Customers
                              .FirstOrDefault(x => x.Id == currentUser.Id)
                              ?? throw new ServiceValidationException("Customer not found");

            if (!currentUser.IsSuperAdmin || (currentUser.Id == res.Id))
            {
                resturant = _context.Resturants
                                    .FirstOrDefault(a => a.Id == addRate.ResturantId)
                                    ?? throw new ServiceValidationException("you have no access to rate any restaurant");
            }

            Rate rate = null;

            if (addRate.Id > 0)
            {
                rate = _context.Rates
                               .FirstOrDefault(a => a.Id == addRate.Id)
                               ?? throw new ServiceValidationException("Invalid rate id received");

                rate.RateNumber = addRate.RateNumber;
                rate.Review = addRate.Review;
                rate.UpdatedBy = res.Id;
                rate.CustomerId = res.Id;
                rate.ResturantId = addRate.ResturantId;
                rate.UpdatedDate = DateTime.Now;
            }
            else
            {
                rate = _context.Rates.Add(new Rate
                {
                    RateNumber = addRate.RateNumber,
                    Review = addRate.Review,
                    CreatedBy = res.Id,
                    CustomerId = res.Id,
                    ResturantId = addRate.ResturantId,
                    CreatedDate = DateTime.Now
                }).Entity;

                _context.SaveChanges();
            }

            resturant.Rates.Add(rate);
            resturant.UpdatedDate = DateTime.UtcNow;
            resturant.UpdatedBy = currentUser.Id;

            _context.SaveChanges();
            return _mapper.Map<AddRateModelView>(rate);
        }

        public RateResponse ViewCustomerRate(UserModelViewModel currentUser,
                                             int page = 1,
                                             int pageSize = 5,
                                             string sortColumn = "",
                                             string sortDirection = "ascending",
                                             string searchText = "")
        {
            var checkCoustomer = _context.Customers
                                        .FirstOrDefault(x => x.Id == currentUser.Id)
                                        ?? throw new ServiceValidationException("You have no access to view rating for this customer");

            if (currentUser.IsSuperAdmin || (checkCoustomer.Id == currentUser.Id))
            {

                var rate = _context.Rates
                                   .FirstOrDefault(x => x.CustomerId == currentUser.Id)
                                   ?? throw new ServiceValidationException("There is no rating for this customer");
            }

            var queryRes = _context.Rates
                                   .Where(a => (string.IsNullOrWhiteSpace(searchText)
                                                || (a.Review.Contains(searchText)
                                                || (a.RateNumber.ToString().Contains(searchText))))
                                                && (a.CustomerId == currentUser.Id));

            if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderBy(sortColumn);
            }
            else if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderByDescending(sortColumn);
            }

            var res = queryRes.GetPaged(page, pageSize);

            var data = new RateResponse
            {
                Rate = _mapper.Map<PagedResult<RateResult>>(res)
            };

            data.Rate.Sortable.Add("RateNumber", "Rate Number");
            data.Rate.Sortable.Add("CreatedDate", "Created Date");

            return data;
        }

        public RateResponse ViewResturantRate(UserModelViewModel currentUser,
                                              int page = 1,
                                              int pageSize = 5,
                                              string sortColumn = "",
                                              string sortDirection = "ascending",
                                              string searchText = "")
        {

            var checkResturant = _context.Resturants
                                         .FirstOrDefault(x => x.Id == currentUser.Id)
                                         ?? throw new ServiceValidationException("You have no access to view rating for this restaurant");

            if (currentUser.IsSuperAdmin || (checkResturant.Id == currentUser.Id))
            {

                var rate = _context.Rates
                                   .FirstOrDefault(x => x.ResturantId == currentUser.Id)
                                   ?? throw new ServiceValidationException("There is no rating for this restaurant");
            }

            var queryRes = _context.Rates
                                   .Where(a => (string.IsNullOrWhiteSpace(searchText)
                                                || (a.Review.Contains(searchText)
                                                || (a.RateNumber.ToString().Contains(searchText))))
                                                && (a.ResturantId == currentUser.Id));

            if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderBy(sortColumn);
            }
            else if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderByDescending(sortColumn);
            }

            var res = queryRes.GetPaged(page, pageSize);

            var data = new RateResponse
            {
                Rate = _mapper.Map<PagedResult<RateResult>>(res)
            };

            data.Rate.Sortable.Add("RateNumber", "Rate Number");
            data.Rate.Sortable.Add("CreatedDate", "Created Date");

            return data;

        }

        public RateResponse ViewResturantRateForUser(UserModelViewModel currentUser, int page = 1, int pageSize = 5, string sortColumn = "", string sortDirection = "ascending", string searchText = "")
        {
            var checkUser = _context.Users
                                    .FirstOrDefault(x => x.Id == currentUser.Id)
                                    ?? throw new ServiceValidationException("You have no access to view rating for any restaurant");

            var queryRes = _context.Rates
                                   .Where(a => (string.IsNullOrWhiteSpace(searchText)
                                                || (a.Review.Contains(searchText)
                                                || (a.RateNumber.ToString().Contains(searchText)))));

            if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderBy(sortColumn);
            }
            else if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderByDescending(sortColumn);
            }

            var res = queryRes.GetPaged(page, pageSize);

            var resturantIds = res.Data
                             .Select(a => a.ResturantId)
                             .Distinct()
                             .ToList();

            var resturants = _context.Resturants
                                     .Where(a => resturantIds.Contains(a.Id))
                                     .ToDictionary(a => a.Id, x => _mapper.Map<ResturantViewAllModelView>(x));

            var data = new RateResponse
            {
                Rate = _mapper.Map<PagedResult<RateResult>>(res),
                Resturant = resturants
            };

            data.Rate.Sortable.Add("RateNumber", "Rate Number");
            data.Rate.Sortable.Add("CreatedDate", "Created Date");

            return data;
        }
    }
}
