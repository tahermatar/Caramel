using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Common.Helperr;
using Caramel.Data;
using Caramel.EmailService;
using Caramel.Infrastructure;
using Caramel.Models;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Meal;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.MealManager
{
    public class MealManager : IMealManager
    {

        private readonly CaramelDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfigurationSettings _configurationSettings;
        private readonly IEmailSender _emailSender;



        public MealManager(CaramelDbContext context, IMapper mapper, IEmailSender emailSender, IConfigurationSettings configurationSettings)
        {
            _context = context;
            _mapper = mapper;
            _emailSender = emailSender;
            _configurationSettings = configurationSettings;
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
            var image = "https://localhost:44309/api/v1/user/fileretrive/profilepic?filename=profileimages/85cd9e2c8cd1457b8cd53592ea7393f7Logo.png";

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
                item.MealCategoryId = vm.MealCategoryId;
                item.ServiceCategoryId = vm.ServiceCategoryId;  
                item.Image = image;

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
                    MealCategoryId = vm.MealCategoryId,
                    ServiceCategoryId = vm.ServiceCategoryId,
                    Image = image
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
                                        int page = 1,
                                        int pageSize = 10,
                                        string sortColumn = "",
                                        string sortDirection = "ascending",
                                        string searchText = "")
        {
            var queryRes = _context.Meals
                                         .Where(a => (string.IsNullOrWhiteSpace(searchText)
                                           || (a.MealName.Contains(searchText)
                                           || a.Price.ToString().Contains(searchText)
                                           || a.MealCategory.ToString().Contains(searchText)
                                           )));

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

            data.Meals.Sortable.Add("Title", "Title");
            data.Meals.Sortable.Add("CreatedDate", "Created Date");

            return data;

        }
        #endregion
    }
}
