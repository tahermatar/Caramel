using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Common.Helperr;
using Caramel.Data;
using Caramel.EmailService;
using Caramel.Infrastructure;
using Caramel.Models;
<<<<<<< HEAD
using Caramel.ModelViews.Enums;
using Caramel.ModelViews.Order;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.Static;
=======
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;
using Microsoft.EntityFrameworkCore;
>>>>>>> development
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Caramel.Core.Mangers.ResturantManager
{
    public class ResturantManager : IResturantManager
    {
        private readonly CaramelDbContext _caramelDbContext;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IConfigurationSettings _configurationSettings;

        public ResturantManager(CaramelDbContext caramelDbContext, IMapper mapper, IEmailSender emailSender, IConfigurationSettings configurationSettings)
        {
            _caramelDbContext = caramelDbContext;
            _mapper = mapper;
            _emailSender = emailSender;
            _configurationSettings = configurationSettings;
        }
        public void DeleteResturant(UserModelViewModel currentResturant, int id)
        {
            if (currentResturant.Id != id)
            {
                throw new ServiceValidationException("you have no access to delete this resturant");
            }

            var resturant = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Id == id)
                                             ?? throw new ServiceValidationException("User not found");

            resturant.Archived = true;
            _caramelDbContext.SaveChanges();
        }

        public ResturantLoginResponseModelView Login(ResturantLoginModelView resturantLogin)
        {
            var resturant = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Email
                                                                   .ToLower().Equals(resturantLogin.Email
                                                                                                   .ToLower()));

            if (resturant == null || !VerifyHashPassword(resturantLogin.Password, resturant.Password))
            {
                throw new ServiceValidationException(300, "Invalid username or password received");
            }

            var res = _mapper.Map<ResturantLoginResponseModelView>(resturant);
            res.Token = $"Bearer {GenerateJwtTaken(resturant)}";

            return res;
        }

        public ResturantLoginResponseModelView SignUp(ResturantRegisterViewModel resturantReg)
        {
            if (_caramelDbContext.Resturants.Any(x => x.Email
                                                       .ToLower() == resturantReg.Email
                                                                                 .ToLower()))
            {
                throw new ServiceValidationException(300, "Resturant Already Exist");
            }

            var hashedPassword = HashPassword(resturantReg.Password);

            var resturant = _caramelDbContext.Resturants.Add(new Resturant
            {
                Name = resturantReg.Name,
                UserName = resturantReg.UserName,
                Email = resturantReg.Email.ToLower(),
                Password = hashedPassword,
                ConfirmPassword = hashedPassword,
<<<<<<< HEAD
                UserName = resturantReg.UserName,
=======
                Address = "",
                Image = "",
                Bio = "",
                Phone = "",
                RoleId = 4,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                TotalRate = 1,
                IsChef = 0,
>>>>>>> development
                ConfirmationLink = Guid.NewGuid().ToString().Replace("-", "").ToString()
            }).Entity;

            _caramelDbContext.SaveChanges();

            var builder = new EmailBuilder(ActionInvocationTypeEnum.EmailConfirmation,
                                new Dictionary<string, string>
                                {
                                    { "AssigneeName", $"{resturantReg.Name}" },
                                    { "Link", $"{resturant.ConfirmationLink}" }
                                }, "https://localhost:44369/");

            var message = new Message(new string[] { resturant.Email }, builder.GetTitle(), builder.GetBody());
            _emailSender.SendEmail(message);

            var result = _mapper.Map<ResturantLoginResponseModelView>(resturant);
            result.Token = $"Bearer {GenerateJwtTaken(resturant)}";

            return result;
        }

<<<<<<< HEAD
        public ResturantModelView Confirmation(string ConfirmationLink)
        {
            var resturant = _caramelDbContext.Resturants
                           .FirstOrDefault(a => a.ConfirmationLink
                                                    .Equals(ConfirmationLink)
                                                && !a.EmailConfirmed)
                       ?? throw new ServiceValidationException("Invalid or expired confirmation link received");

            resturant.EmailConfirmed = true;
            resturant.ConfirmationLink = string.Empty;
            _caramelDbContext.SaveChanges();
            return _mapper.Map<ResturantModelView>(resturant);
        }

        public ResturantModelView UpdateProfile(ResturantModelView currentResturant, ResturantModelView request)
=======
        public ResturantModelView UpdateProfile(UserModelViewModel currentResturant, ResturantModelView request)
>>>>>>> development
        {
            var resturant = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Id == currentResturant.Id)
                                             ?? throw new ServiceValidationException("Resturant not found");

            var url = "";

            if (!string.IsNullOrWhiteSpace(request.ImageString))
            {
                url = Helper.SaveImage(request.ImageString, "ProfileImages");
            }

            resturant.Name = request.Name;
            resturant.Email = request.Email;
            resturant.Bio = request.Bio;

            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44309/";
                resturant.Image = @$"{baseURL}/api/resturant/fileretrive/profilepic?filename={url}";
            }

            _caramelDbContext.SaveChanges();
            return _mapper.Map<ResturantModelView>(resturant);
        }

<<<<<<< HEAD
        public MealCategoryModelView PutMealCategory(ResturantModelView currentResturant, CategoryRequest categoryRequest)
        {
            MealCategory mealCategory = null;

            if (categoryRequest.Id > 0)
            {
                mealCategory = _caramelDbContext.MealCategories
                                    .FirstOrDefault(a => a.Id == mealCategory.Id)
                                    ?? throw new ServiceValidationException("Invalid meal category id received");

                mealCategory.CategoryName = categoryRequest.CategoryName;
                mealCategory.ExtraInformation = categoryRequest.ExtraInformation;
            }
            else
            {
                mealCategory = _caramelDbContext.MealCategories.Add(new MealCategory
                {
                    CategoryName = categoryRequest.CategoryName,
                    ExtraInformation = categoryRequest.ExtraInformation
                }).Entity;
            }

            _caramelDbContext.SaveChanges();
            return _mapper.Map<MealCategoryModelView>(mealCategory);
        }
        public MealModelView PutMeal(ResturantModelView currentResturant, MealRequest mealRequest)
        {
            Meal meal = null;

            if (mealRequest.Id > 0)
            {
                meal = _caramelDbContext.Meals
                                    .FirstOrDefault(a => a.Id == mealRequest.Id)
                                    ?? throw new ServiceValidationException("Invalid meal id received");

                meal.MealName = mealRequest.MealName;
                meal.ServiceCategoryId = mealRequest.ServiceCategoryId;
                meal.MealCategoryId = mealRequest.MealCategoryId;
                meal.Price = mealRequest.Price;
                meal.Quantity = mealRequest.Quantity;
                meal.Component = mealRequest.Component;
                meal.IsAvailable = mealRequest.IsAvailable;
                meal.ImageId = mealRequest.ImageId;
            }
            else
            {
                meal = _caramelDbContext.Meals.Add(new Meal
                {
                    MealName = mealRequest.MealName,
                    MealCategoryId = mealRequest.MealCategoryId,
                    ServiceCategoryId = mealRequest.ServiceCategoryId,
                    ImageId = mealRequest.ImageId,
                    Price = mealRequest.Price,
                    Quantity = mealRequest.Quantity,
                    Component = mealRequest.Component,
                    IsAvailable = mealRequest.IsAvailable
                }).Entity;
            }

            _caramelDbContext.SaveChanges();
            return _mapper.Map<MealModelView>(meal);
        }
        public ImageModelView PutImage(ResturantModelView currentResturant, ImageRequest imageRequest)
        {
            Image image = null;
            var url = "";

            if (!string.IsNullOrWhiteSpace(imageRequest.ImageString))
            {
                url = Helper.SaveImage(imageRequest.ImageString, "ProfileImages");
            }

            if (imageRequest.Id > 0)
            {
                image = _caramelDbContext.Images
                                    .FirstOrDefault(a => a.Id == imageRequest.Id)
                                    ?? throw new ServiceValidationException("Invalid image id received");

                image.Image1 = imageRequest.Image1;
                image.Title = imageRequest.Title;
                image.ExtraInformation = imageRequest.ExtraInformation; 
            }
            else
            {
                image = _caramelDbContext.Images.Add(new Image
                {
                    Image1 = imageRequest.Image1,
                    Title = imageRequest.Title,
                    ExtraInformation = imageRequest.ExtraInformation
                }).Entity;
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44309/";
                image.Image1 = @$"{baseURL}/api/resturant/fileretrive/profilepic?filename={url}";
            }

            _caramelDbContext.SaveChanges();
            return _mapper.Map<ImageModelView>(image);
        }

        public ServiceCategoryModelView PutServiceCategory(ResturantModelView currentResturant, ServiceCategoryRequest serviceCategoryRequest)
        {
            ServiceCategory service = null;

            if (serviceCategoryRequest.Id > 0)
            {
                service = _caramelDbContext.ServiceCategories
                                    .FirstOrDefault(a => a.Id == serviceCategoryRequest.Id)
                                    ?? throw new ServiceValidationException("Invalid service id received");

                service.CategoryName = serviceCategoryRequest.CategoryName;
                service.ExtraInformation = serviceCategoryRequest.ExtraInformation;
            }
            else
            {
                service = _caramelDbContext.ServiceCategories.Add(new ServiceCategory
                {
                    CategoryName = serviceCategoryRequest.CategoryName,
                    ExtraInformation = serviceCategoryRequest.ExtraInformation
            }).Entity;
            }

            _caramelDbContext.SaveChanges();
            return _mapper.Map<ServiceCategoryModelView>(service);
        }

        #region private
        private static string HashPassword(string password)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                return hashedPassword;
            }

            private static bool VerifyHashPassword(string password, string HashedPassword)
            {
                return BCrypt.Net.BCrypt.Verify(password, HashedPassword);
            }

            private string GenerateJwtTaken(Resturant resturant)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationSettings.JwtKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub , $"{resturant.UserName}"),
                new Claim(JwtRegisteredClaimNames.Email , resturant.Email ),
                new Claim("Id" , resturant.Id.ToString() ),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString() ),
                };

                var taken = new JwtSecurityToken(
                    _configurationSettings.Issuar,
                    _configurationSettings.Issuar,
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(taken);
                #endregion private
            }
    }
=======
        public ResturantModelView ViewProfile(UserModelViewModel currentUser)
        {
            var res = _caramelDbContext.Resturants.FirstOrDefault(x => x.Id == currentUser.Id)
                ?? throw new ServiceValidationException("User not found");

            if (res == null)
            {
                throw new ServiceValidationException("User not found");
            }


            return _mapper.Map<ResturantModelView>(res);
        }



        #region private
        private static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashedPassword;
        }

        private static bool VerifyHashPassword(string password, string HashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashedPassword);
        }

        private string GenerateJwtTaken(Resturant user)
        {
            var jwtKey = "#test.key*j;ljklkjhadfsd";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , $"{user.UserName}"),
                new Claim(JwtRegisteredClaimNames.Email , user.Email ),
                new Claim("Id" , user.Id.ToString() ),
                new Claim("Resturant_Name", user.Name),
                new Claim("DateOfJoining", user.CreatedDate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString() ),
            };
            var issuer = "test.com";
            var taken = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(taken);

        }
        #endregion
>>>>>>> development
    }
