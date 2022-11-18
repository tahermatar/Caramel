using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Common.Helperr;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews.Resturant;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
        public void DeleteResturant(ResturantModelView currentResturant, int id)
        {
            if (currentResturant.Id != id)
            {
                throw new ServiceValidationException("you have no access to delete this resturant");
            }

            var resturant = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Id == id)
                                             ?? throw new ServiceValidationException("User not found");
            //var resturant = _caramelDbContext.Resturants.FirstOrDefault(x => x.Email == currentResturant.Email
            //                                                              && x.Password == currentResturant.Password)
            //                                                              ?? throw new ServiceValidationException("");

            resturant.Archived = false;
            _caramelDbContext.SaveChanges();
        }

        public ResturantLoginResponseModelView Login(ResturantLoginModelView resturantLogin)
        {
            var resturant = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Email
                                                                   .ToLower().Equals(resturantLogin.Email
                                                                                                   .ToLower()));

            if(resturant == null || !VerifyHashPassword(resturantLogin.Password, resturant.Password))
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
                UserName = resturantReg.UserName
            }).Entity;

            _caramelDbContext.SaveChanges();

            var result = _mapper.Map<ResturantLoginResponseModelView>(resturant);
            result.Token = $"Bearer {GenerateJwtTaken(resturant)}";

            return result;
        }

        public ResturantModelView UpdateProfile(ResturantModelView currentResturant, ResturantModelView request)
        {
            var resturant = _caramelDbContext.Resturants
                                             .FirstOrDefault(x => x.Id == currentResturant.Id)
                                             ?? throw new ServiceValidationException("User not found");

            var url = "";

            if (!string.IsNullOrWhiteSpace(request.ImageString))
            {
                url = Helper.SaveImage(request.ImageString, "Profile Images");
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
            var jwtKey = "#test.key*j;ljklkjhadfsd";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , $"{resturant.UserName}"),
                new Claim(JwtRegisteredClaimNames.Email , resturant.Email ),
                new Claim("Id" , resturant.Id.ToString() ),
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
            #endregion
        }
    }
}
