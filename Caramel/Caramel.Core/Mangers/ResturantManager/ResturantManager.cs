using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Common.Helperr;
using Caramel.Data;
using Caramel.EmailService;
using Caramel.Infrastructure;
using Caramel.Models;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Enums;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.Static;
using Caramel.ModelViews.User;
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

        public ResturantManager(CaramelDbContext caramelDbContext,
                                IMapper mapper,
                                IEmailSender emailSender,
                                IConfigurationSettings configurationSettings)
        {
            _caramelDbContext = caramelDbContext;
            _mapper = mapper;
            _emailSender = emailSender;
            _configurationSettings = configurationSettings;
        }

        #region public
        public ResturantLoginResponseModelView SignUp(UserModelViewModel currentUser,
                                                      ResturantRegisterViewModel resturantReg)
        {
            //var cretorId = 0;
            //if (currentUser != null)
            //{
            //    cretorId = currentUser.Id;
            //}

            if (_caramelDbContext.Resturants.Any(x => x.Email
                                                       .ToLower() == resturantReg.Email
                                                                                 .ToLower()))
            {
                throw new ServiceValidationException(300, "Resturant Already Exist");
            }

            var hashedPassword = HashPassword(resturantReg.Password);

            var url = "";
            var image = "";

            if (!string.IsNullOrWhiteSpace(resturantReg.ImageString))
            {
                url = Helper.SaveImage(resturantReg.ImageString, "profileimages");
            }


            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44309/";
                resturantReg.Image = @$"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
                image = resturantReg.Image;
            }

            var address = _caramelDbContext.Addresses.Add(new Address
            {
                //ResturantId = currentUser.Id,
                City = resturantReg.City,
                Country = resturantReg.Country,
                Road = resturantReg.Road,
                ExtraInformation = resturantReg.ExtraAddressInformation
            }).Entity;

            _caramelDbContext.SaveChanges();

            var resturant = _caramelDbContext.Resturants.Add(new Resturant
            {
                Name = resturantReg.Name,
                UserName = resturantReg.UserName,
                Email = resturantReg.Email.ToLower(),
                Password = hashedPassword,
                ConfirmPassword = hashedPassword,
                Address = address.Id,
                IsChef = resturantReg.IsChef,
                Image = image,
                Bio = "",
                Phone = "",
                RoleId = 4,
                //CreatedBy = cretorId,
                CreatedDate = DateTime.Now,
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

        public ResturantRegViewModel Confirmation(UserModelViewModel currentUser,
                                                  string ConfirmationLink)
        {
            var res = _caramelDbContext.Resturants
                                       .FirstOrDefault(a => a.ConfirmationLink
                                                             .Equals(ConfirmationLink)
                                                             && !a.EmailConfirmed)
           ?? throw new ServiceValidationException("Invalid or expired confirmation link received");

            res.EmailConfirmed = true;
            res.ConfirmationLink = string.Empty;
            _caramelDbContext.SaveChanges();
            return _mapper.Map<ResturantRegViewModel>(res);
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

        public ResturantViewAllModelView ViewProfile(UserModelViewModel currentUser)
        {

            var resturant = _caramelDbContext.Resturants.FirstOrDefault(x => x.Id == currentUser.Id)
                            ?? throw new ServiceValidationException("You have no access to view this resturant");

            if (resturant == null)
            {
                throw new ServiceValidationException("User not found");
            }

            var address = _caramelDbContext.Addresses.FirstOrDefault(x => x.Id == resturant.Address);

            var profileData =  _mapper.Map<ResturantViewAllModelView>(resturant);

            profileData.City = address.City;
            profileData.Country = address.Country;
            profileData.Road = address.Road;
            profileData.ExtraAddressInformation = address.ExtraInformation;

            return profileData;
        }

        public ResturantViewAllModelView GetResturant(int id)
        {
            var res = _caramelDbContext.Resturants.FirstOrDefault(a => a.Id == id)
                      ?? throw new ServiceValidationException("Invalid resturant id received");

            return _mapper.Map<ResturantViewAllModelView>(res);
        }

        public ResturantResponse GetAll(UserModelViewModel currentUser,
                                         int page = 1,
                                         int pageSize = 10,
                                         string sortColumn = "",
                                         string sortDirection = "ascending",
                                         string searchText = "")
        {

            var queryRes = _caramelDbContext.Resturants
                                                      .Where(a => (string.IsNullOrWhiteSpace(searchText)
                                                            || (a.UserName.Contains(searchText)
                                                            || a.Email.Contains(searchText))));

            if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderBy(sortColumn);
            }
            else if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderByDescending(sortColumn);
            }

            var res = queryRes.GetPaged(page, pageSize);

            var data = new ResturantResponse
            {
                Resturants = _mapper.Map<PagedResult<ResturantModelView>>(res)
            };

            data.Resturants.Sortable.Add("Title", "Title");
            data.Resturants.Sortable.Add("CreatedDate", "Created Date");

            return data;
        }

        public ResturantUpdateModelView UpdateProfile(UserModelViewModel currentResturant,
                                                       ResturantModelView request)
        {
            var resturant = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Id == currentResturant.Id)
                                             ?? throw new ServiceValidationException("You have no access to update this resturant");

            var url = "";

            if (!string.IsNullOrWhiteSpace(request.ImageString))
            {
                url = Helper.SaveImage(request.ImageString, "profileimages");
            }

            var image = "";

            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44309/";
                request.Image = @$"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
                image = request.Image;

            }

            _caramelDbContext.SaveChanges();

            resturant.Image = image;
            resturant.ServiceCategoryId = request.ServiceCategoryId;
            resturant.Bio = request.Bio;
            resturant.WorkingTime = request.WorkingTime;
            resturant.IsChef = request.IsChef;
            resturant.Phone = request.Phone;
            resturant.UpdatedBy = currentResturant.Id;
            resturant.UpdatedDate = DateTime.Now;

            _caramelDbContext.SaveChanges();

            return _mapper.Map<ResturantUpdateModelView>(resturant);
        }

        public ResturantModelView UpdateResturantAddress(UserModelViewModel currentUser,
                                                         AddressResult reg)
        {
            var res = _caramelDbContext.Resturants
                                       .FirstOrDefault(x => x.Id == currentUser.Id)
                                       ?? throw new ServiceValidationException("You have no access to add or update address for this resturant");

            Address item = null;

            if (reg.Id > 0)
            {
                item = _caramelDbContext.Addresses
                                        .FirstOrDefault(a => a.Id == reg.Id)
                                        ?? throw new ServiceValidationException("Invalid Address id received");

                item.City = reg.City;
                item.Country = reg.Country;
                item.Road = reg.Road;
                item.ExtraInformation = reg.ExtraInformation;
            }

            else
            {
                item = _caramelDbContext.Addresses.Add(new Address
                {
                    ResturantId = currentUser.Id,
                    City = reg.City,
                    Country = reg.Country,
                    Road = reg.Road,
                    ExtraInformation = reg.ExtraInformation
                }).Entity;

                _caramelDbContext.SaveChanges();
            }

            res.Address = item.Id;
            res.UpdatedBy = currentUser.Id;
            res.UpdatedDate = DateTime.UtcNow;

            _caramelDbContext.SaveChanges();

            var c = _mapper.Map<ResturantModelView>(res);

            c.City = item.City;
            c.Country = item.Country;
            c.Road = item.Road;
            c.ExtraAddressInformation = item.ExtraInformation;

            return c;
        }

        public ResturantRegViewModel UpdateRegistrationData(UserModelViewModel currentUser,
                                                            ResturantUpdateRegModelView reg)
        {
            var res = _caramelDbContext.Resturants.FirstOrDefault(x => x.Id == currentUser.Id)
                                                  ?? throw new ServiceValidationException("You have no access to update registration data for this resturant");

            if (res == null)
            {
                throw new ServiceValidationException("User not found");
            }

            var hashedPassword = HashPassword(reg.Password);

            res.UserName = reg.UserName;
            res.UpdatedDate = DateTime.Now;
            res.UpdatedBy = currentUser.Id;
            res.Email = reg.Email;
            res.Name = reg.Name;
            res.Password = hashedPassword;
            res.ConfirmPassword = hashedPassword;

            _caramelDbContext.SaveChanges();
            return _mapper.Map<ResturantRegViewModel>(res);
        }

        public void DeleteResturant(UserModelViewModel currentResturant, int id)
        {
            if (currentResturant.Id != id)
            {
                throw new ServiceValidationException("you have no access to delete this resturant");
            }

            var resturant = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Id == id)
                                             ?? throw new ServiceValidationException("Resturant not found");

            resturant.Archived = true;
            _caramelDbContext.SaveChanges();

            //var user = new Resturant();

            //if (currentResturant.IsSuperAdmin)
            //{
            //    user = _caramelDbContext.Resturants
            //                             .FirstOrDefault(x => x.Id == id)
            //                              ?? throw new ServiceValidationException("User not found");
            //}
            //else
            //{
            //    throw new ServiceValidationException("you have no access to delete your self");
            //}


            //user.Archived = true;
            //_caramelDbContext.SaveChanges();
        }

        #endregion

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
    }
}
