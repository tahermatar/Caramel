using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Common.Helperr;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews;
using Caramel.ModelViews.Meal;
using Caramel.ModelViews.User;
using System;
using System.Linq;


namespace Caramel.Core.Mangers.MealManager
{
    public class MealManager : IMealManager
    {

        private readonly CaramelDbContext _context;
        private readonly IMapper _mapper;

        public MealManager(CaramelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        #region public

        public MealCreateModelView PutMeal(UserModelViewModel currentUser, MealCreateModelView vm)
        {
            var res = new Resturant();

            if (currentUser.IsSuperAdmin)
            {
                res = _context.Resturants
                                         .FirstOrDefault(x => x.Id == vm.ResturantId)
                                          ?? throw new ServiceValidationException("Resturant not found");
            }
            else {
                res = _context.Resturants
                                         .FirstOrDefault(x => x.Id == currentUser.Id)
                                          ?? throw new ServiceValidationException("Resturant not found");
            }

            var url = "";
            var image = "";

            if (!string.IsNullOrWhiteSpace(vm.ImageString))
            {
                url = Helper.SaveImage(vm.ImageString, "profileimages");
            }


            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44309/";
                vm.Image = @$"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
                image = vm.Image;
            }


            Meal item = null;

            if (vm.Id > 0)
            {
                item = _context.Meals
                                .FirstOrDefault(a => a.Id == vm.Id)
                                 ?? throw new ServiceValidationException("Invalid Meal id received");


                item.Price = vm.Price;
                item.Component = vm.Component;
                item.IsAvailable = vm.IsAvailable;
                item.MealName = vm.MealName;
                item.Quantity = vm.Quantity;
                item.UpdatedBy = currentUser.Id;
                item.UpdatedDate = DateTime.UtcNow;
                item.ServiceCategoryId = (int)vm.ServiceCategory;  
                item.Image = image;
                item.MealCategoryId = (int)vm.MealCat;

            }
            else
            {

                item = _context.Meals.Add(new Meal
                {
                    Price = vm.Price,
                    Component = vm.Component,
                    IsAvailable = vm.IsAvailable,
                    MealName = vm.MealName,
                    Quantity = vm.Quantity,
                    CreatedBy = res.Id,
                    CreatedDate = DateTime.UtcNow,
                    ServiceCategoryId = (int)vm.ServiceCategory,
                    Image = image,
                    MealCategoryId = (int)vm.MealCat
            }).Entity;
                _context.SaveChanges();

            }

            res.Meals.Add(item);
            res.UpdatedDate = DateTime.UtcNow;
            res.UpdatedBy = currentUser.Id;

            _context.SaveChanges();
            return _mapper.Map<MealCreateModelView>(item);

        }

        public MealResponse GetResturantAllMeal(UserModelViewModel currentUser,
                                                int ResturantId,
                                                MealCategoryEnum MealCat = MealCategoryEnum.All,
                                                ServiceCategoryEnum ServiceCat = ServiceCategoryEnum.All,
                                                int page = 1,
                                                int pageSize = 10,
                                                string sortColumn = "",
                                                string sortDirection = "ascending",
                                                string searchText = "")
        {

            IQueryable<Meal> queryRes;

            if (ResturantId > 0)
            {

                var resturantt = _context.Meals
                                         .FirstOrDefault(a => a.ResturantId == ResturantId)
                                         ?? throw new ServiceValidationException(300,"Invalid Resturant id received");

                queryRes = _context.Meals
                                   .Where(a => ((string.IsNullOrWhiteSpace(searchText)
                                   || (a.MealName.Contains(searchText)
                                   || a.Price.ToString().Contains(searchText)))
                                   && (a.ResturantId == ResturantId)
                                   && (MealCat == MealCategoryEnum.All
                                    || a.MealCategoryId == (int)MealCat)
                                   && (ServiceCat == ServiceCategoryEnum.All
                                    || a.ServiceCategoryId == (int)ServiceCat)
                                   ));
            }
            else {
                queryRes = _context.Meals
                                   .Where(a => ((string.IsNullOrWhiteSpace(searchText)
                                   || (a.MealName.Contains(searchText)
                                   || a.Price.ToString().Contains(searchText)))
                                   && (MealCat == MealCategoryEnum.All
                                   || a.MealCategoryId == (int)MealCat)
                                   && (ServiceCat == ServiceCategoryEnum.All
                                   || a.ServiceCategoryId == (int)ServiceCat)
                                   ));
            }

           

            if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderBy(sortColumn);
            }
            else if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderByDescending(sortColumn);
            }

            var res = queryRes.GetPaged(page, pageSize);

            var data = new MealResponse
            {
                Meals = _mapper.Map<PagedResult<MealResult>>(res)
            };

            data.Meals.Sortable.Add("MealName", "Meal Name");
            data.Meals.Sortable.Add("CreatedDate", "Created Date");
            data.Meals.Filterable.Add("MealCategoryId", new FilterableKeyModel
            {
                Title = "MealCategory",
                Values = ((MealCategoryEnum[])Enum.GetValues(typeof(MealCategoryEnum)))
                            .Select(c => new FilterableValueModel
                            {
                                Id = (int)c,
                                Title = c.GetDescription().ToString()
                            })
                            .ToList()
            });

            data.Meals.Filterable.Add("ServiceCategoryId", new FilterableKeyModel
            {
                Title = "ServiceCategory",
                Values = ((ServiceCategoryEnum[])Enum.GetValues(typeof(ServiceCategoryEnum)))
                            .Select(c => new FilterableValueModel
                            {
                                Id = (int)c,
                                Title = c.GetDescription().ToString()
                            })
                            .ToList()
            });

            return data;

        }


        public MealModelView viewMeal(int id)
        {
            var meal = _context.Meals
                                    .FirstOrDefault(a => a.Id == id)
                                     ?? throw new ServiceValidationException("Invalid Meal id received");

            return _mapper.Map<MealModelView>(meal);
        }


        public void DeleteMeal(UserModelViewModel currentUser, int id)
        {

            var res = _context.Meals
                                    .FirstOrDefault(x => x.Id == id)
                                    ?? throw new ServiceValidationException("Meal not found");

            if ((res.ResturantId == currentUser.Id) || currentUser.IsSuperAdmin)
            {
                res.Archived = true;
                _context.SaveChanges();
            }
            else 
            { 
                throw new ServiceValidationException(300, "you have no access to delete the restaurant meals");
            }
        }


        #endregion
    }
}
