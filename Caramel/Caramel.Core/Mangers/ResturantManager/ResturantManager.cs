using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Common.Helperr;
using Caramel.Data;
using Caramel.EmailService;
using Caramel.Infrastructure;
using Caramel.Models;
using Caramel.ModelViews.Enums;
using Caramel.ModelViews.Order;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.Static;
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
        public void DeleteResturant(ResturantModelView currentResturant, int id)
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
                Email = resturantReg.Email,
                Password = hashedPassword,
                ConfirmPassword = hashedPassword,
                UserName = resturantReg.UserName,
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

        public List<OrderResult> GetAll()
        {
            //if(_caramelDbContext.Orders.Any(x => x.ResturantId == )
            //{
            //    throw new ServiceValidationException(300, "You don't have access to view all orders for this restaurant");
            //}
            var orderList = _caramelDbContext.Orders.ToList();
            return _mapper.Map<List<OrderResult>>(orderList);
            
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
                _configurationSettings.Issuer,
                _configurationSettings.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(taken);
            #endregion
        }

        
    }
}
