using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Common.Helperr;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Caramel.Core.Mangers.ResturantManager
{
    public class ResturantManager : IResturantManager
    {
        private readonly CaramelDbContext _caramelDbContext;
        private readonly IMapper _mapper;

        public ResturantManager(CaramelDbContext caramelDbContext, IMapper mapper)
        {
            _caramelDbContext = caramelDbContext;
            _mapper = mapper;
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

            resturant.Archived = false;
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

        public ResturantLoginResponseModelView SignUp(UserModelViewModel currentUser,
                                                      ResturantRegisterViewModel resturantReg)
        {
            var cretorId = 0;
            if (currentUser != null)
            {
                cretorId = currentUser.Id;
            }

            if (_caramelDbContext.Resturants.Any(x => x.Email
                                                       .ToLower() == resturantReg.Email
                                                                                 .ToLower()))
            {
                throw new ServiceValidationException(300, "Resturant Already Exist");
            }

            var hashedPassword = HashPassword(resturantReg.Password);

            var url = "";
            var image = "https://localhost:44309//api/v1/user/fileretrive/profilepic?filename=profileimages\\85cd9e2c8cd1457b8cd53592ea7393f7Logo.png";

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

           

            var resturant = _caramelDbContext.Resturants.Add(new Resturant
            {
                Name = resturantReg.Name,
                UserName = resturantReg.UserName,
                Email = resturantReg.Email.ToLower(),
                Password = hashedPassword,
                ConfirmPassword = hashedPassword,
                Address = 1,
                Image = image,
                Bio = "",
                Phone = "",
                RoleId = 4,
                CreatedBy = cretorId,
                CreatedDate = DateTime.Now,
                TotalRate = 1,
                IsChef = 0,
                ConfirmationLink = Guid.NewGuid().ToString().Replace("-", "").ToString()
            }).Entity;

            _caramelDbContext.SaveChanges();

            var result = _mapper.Map<ResturantLoginResponseModelView>(resturant);
            result.Token = $"Bearer {GenerateJwtTaken(resturant)}";

            return result;
        }


        public ResturantViewAllModelView UpdateProfile(UserModelViewModel currentResturant,
                                                ResturantModelView request)
        {
            var user = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Id == currentResturant.Id)
                                             ?? throw new ServiceValidationException("User not found");

            var resturant = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Id == request.Id)
                                             ?? throw new ServiceValidationException("Resturant not found");

            var url = "";

            if (!string.IsNullOrWhiteSpace(request.ImageString))
            {
                url = Helper.SaveImage(request.ImageString, "Profile Images");
            }

            var image = "";

            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44309/";
                request.Image = @$"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
                image = request.Image;

            }



            var address = _caramelDbContext.Addresses.Add(new Address { 
                City = request.City,
                Country = request.Country,
                Road = request.Road,
                ExtraInformation = request.ExtraAddressInformation
            }).Entity;

            resturant.Image = image;
            resturant.ServiceCategoryId = request.ServiceCategoryId;
            resturant.Bio = request.Bio;
            resturant.WorkingTime = request.WorkingTime;
            resturant.IsChef = request.IsChef;
            resturant.Phone = request.Phone;
            resturant.Address = address.Id;
            resturant.UpdatedBy = currentResturant.Id;
            resturant.UpdatedDate = DateTime.Now;   


            _caramelDbContext.SaveChanges();
            return _mapper.Map<ResturantViewAllModelView>(resturant);
        }

        public ResturantViewAllModelView ViewProfile(UserModelViewModel currentUser)
        {
            var res = _caramelDbContext.Resturants.FirstOrDefault(x => x.Id == currentUser.Id)
                ?? throw new ServiceValidationException("User not found");

            if (res == null)
            {
                throw new ServiceValidationException("User not found");
            }

            return _mapper.Map<ResturantViewAllModelView>(res);
        }

        public ResturantRegViewModel UpdateRegistrationData(UserModelViewModel currentUser, ResturantRegViewModel reg)
        {
            var res = _caramelDbContext.Resturants.FirstOrDefault(x => x.Id == currentUser.Id)
                ?? throw new ServiceValidationException("User not found");

            if (res == null)
            {
                throw new ServiceValidationException("User not found");
            }


            var res1 = _caramelDbContext.Resturants.FirstOrDefault(x => x.Id ==
            reg.Id)
                ?? throw new ServiceValidationException("User not found");
            var hashedPassword = HashPassword(reg.Password);

            res1.UserName = reg.UserName;
            res1.UpdatedDate = DateTime.Now;
            res1.UpdatedBy = currentUser.Id;
            res1.Email = reg.Email;
            res1.Name = reg.Name;
            res1.Password = hashedPassword;
            res1.ConfirmPassword = hashedPassword;


            _caramelDbContext.SaveChanges();
            return _mapper.Map<ResturantRegViewModel>(res1);
        }

        public ResturantModelView UpdateResturantAddress(UserModelViewModel currentUser,
                                                    AddressResult reg)
        {

            var res = new Resturant();
            if (currentUser.IsSuperAdmin) { 
                    res = _caramelDbContext.Resturants.FirstOrDefault(x => x.Id == reg.UserId)
                              ?? throw new ServiceValidationException("Customer not found");

                    if (res == null)
                    {
                        throw new ServiceValidationException("User not found");
                    }
            }else { 
                res = _caramelDbContext.Resturants.FirstOrDefault(x => x.Id == currentUser.Id)
                              ?? throw new ServiceValidationException("Customer not found");}

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
