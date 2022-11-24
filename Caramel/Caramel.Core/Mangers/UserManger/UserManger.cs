using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.UserManger
{
    public class UserManger : IUserManger
    {
        private readonly CaramelDbContext _caramelDbContext;
        private readonly IMapper _mapper;

        public UserManger(CaramelDbContext caramelDbContext, IMapper mapper)
        {
            _caramelDbContext = caramelDbContext;
            _mapper = mapper;
        }
        #region public
        public UserLoginResponseViewModel Login(UserLoginViewModel vm)
        {

            var user = _caramelDbContext.Users.FirstOrDefault(x => x.Email.ToLower().Equals(vm.Email.ToLower()));

            if (user == null || !VerifyHashPassword(vm.Password, user.Password))
            {
                throw new ServiceValidationException(300, "User Is not valid Name or password");
            }

            var res = _mapper.Map<UserLoginResponseViewModel>(user);
            res.Token = $"Bearer {GenerateJwtTaken(user)}";

            return res;
        }

        public UserLoginResponseViewModel Rigester(UserRegisterViewModel vm)
        {

            if (_caramelDbContext.Users.Any(x => x.Email.ToLower() == vm.Email.ToLower()))
            {
                throw new ServiceValidationException(300, "User Is Exist");
            }

            var hashed = HashPassword(vm.Password);

            //var user = _csvdbContext.Users.Add(new User
            var res = new User
            {
                UserName = vm.UserName,
                Email = vm.Email.ToLower(),
                Password = hashed,
                ConfirmPassword = hashed,
                CreatedDate = DateTime.Now,
                IsSuperAdmin = vm.IsSuperAdmin,
                Archived = true,
            };
            _caramelDbContext.Users.Add(res);
            _caramelDbContext.SaveChanges();

            var result = _mapper.Map<UserLoginResponseViewModel>(res);
            result.Token = $"Bearer {GenerateJwtTaken(res)}";

            return result;
        }

        public UserModelViewModel UpdateProfile(UserModelViewModel currentUser, UserModelViewModel request)
        {

            var user = _caramelDbContext.Users.FirstOrDefault(x => x.Id == currentUser.Id)
                ?? throw new ServiceValidationException("User not found");

            if (user == null)
            {
                throw new ServiceValidationException("User not found");
            }

            var url = "";


            user.UserName = request.UserName;
            user.UpdatedDate = DateTime.Now;
            user.IsSuperAdmin = request.IsSuperAdmin;

            _caramelDbContext.SaveChanges();

            //var res = new UserModelViewModel
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    Email = user.Email,
            //};
            return _mapper.Map<UserModelViewModel>(user);
            //return res;
        }
        public void DeleteUser(UserModelViewModel currentUser, int id)
        {
            if (currentUser.Id == id)
            {
                throw new ServiceValidationException("you have no access to delete your self");
            }
            var user = _caramelDbContext.Users
                .FirstOrDefault(x => x.Id == id)
                ?? throw new ServiceValidationException("User not found");

            user.Archived = false;

            _caramelDbContext.SaveChanges();
        }
        #endregion public
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

        private string GenerateJwtTaken(User user)
        {
            var jwtKey = "#test.key*j;ljklkjhadfsd";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , $"{user.UserName}"),
                new Claim(JwtRegisteredClaimNames.Email , user.Email ),
                new Claim("Id" , user.Id.ToString() ),
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
        #endregion private

    }
}
