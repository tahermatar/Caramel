﻿using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Data;
using Caramel.EmailService;
using Caramel.Infrastructure;
using Caramel.Models;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Enums;
using Caramel.ModelViews.Static;
using Caramel.ModelViews.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.CustomerManger
{
    public class CustomerManger : ICustomerManger
    {

        private readonly CaramelDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfigurationSettings _configurationSettings;
        private readonly IEmailSender _emailSender;



        public CustomerManger(CaramelDbContext context, IMapper mapper, IEmailSender emailSender, IConfigurationSettings configurationSettings)
            {
                _context = context;
                _mapper = mapper;
                _emailSender = emailSender;
                _configurationSettings = configurationSettings; 
            }

        #region public

        public List<CustomerResult> GetAll()
            {
                var customerList = _context.Customers.ToList();
                return _mapper.Map<List<CustomerResult>>(customerList);
            }

        public CustomerLoginResponseViewModel Rigester(CustomerRegisterViewModel vm)
        {
            if (_context.Customers
                              .Any(a => a.Email.ToLower().Equals(vm.Email.ToLower())))
            {
                throw new ServiceValidationException("Customer already exist");
            }

            var hashedPassword = HashPassword(vm.Password);

            var customer = _context.Customers.Add(new Customer
            {
                Name = vm.Name,
                UserName = vm.UserName,
                Email = vm.Email.ToLower(),
                Password = hashedPassword,
                ConfirmPassword = hashedPassword,
                AddressId = 1,
                Phone = "",
                ConfirmationLink = Guid.NewGuid().ToString().Replace("-", "").ToString()
            }).Entity;

            _context.SaveChanges();
            
            var builder = new EmailBuilder(ActionInvocationTypeEnum.EmailConfirmation,
                                new Dictionary<string, string>
                                {
                                    { "AssigneeName", $"{vm.Name}" },
                                    { "Link", $"{customer.ConfirmationLink}" }
                                }, "https://localhost:44309");

            var message = new Message(new string[] { vm.Email }, builder.GetTitle(), builder.GetBody());
            _emailSender.SendEmail(message);


            var res = _mapper.Map<CustomerLoginResponseViewModel>(customer);
            res.Token = $"Bearer {GenerateJWTToken(customer)}";

            return res;
        }

        public CustomerResult Confirmation(string ConfirmationLink)
        {
            var customer = _context.Customers
                           .FirstOrDefault(a => a.ConfirmationLink
                                                    .Equals(ConfirmationLink)
                                                && !a.EmailConfirmed)
                       ?? throw new ServiceValidationException("Invalid or expired confirmation link received");

            customer.EmailConfirmed = true;
            customer.ConfirmationLink = string.Empty;
            _context.SaveChanges();
            return _mapper.Map<CustomerResult>(customer);
        }

        #endregion


        #region private 

        private static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashedPassword;
        }

        private static bool VerifyHashPassword(string password, string HashedPasword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashedPasword);
        }

        private string GenerateJWTToken(Customer user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationSettings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, $"{user.UserName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim("DateOfJoining", user.CreatedDate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                        _configurationSettings.Issuer,
                        _configurationSettings.Issuer,
                        claims,
                        expires: DateTime.Now.AddDays(20),
                        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        

        #endregion private  
    }
}