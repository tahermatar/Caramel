using AutoMapper;
using Caramel.Common.Extinsions;
using Caramel.Common.Helperr;
using Caramel.Data;
using Caramel.EmailService;
using Caramel.Infrastructure;
using Caramel.Models;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Enums;
using Caramel.ModelViews.Static;
using Caramel.ModelViews.User;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Caramel.Core.Mangers.CustomerManger
{
    public class CustomerManager : ICustomerManager
    {

        private readonly CaramelDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfigurationSettings _configurationSettings;
        private readonly IEmailSender _emailSender;



        public CustomerManager(CaramelDbContext context, IMapper mapper, IEmailSender emailSender, IConfigurationSettings configurationSettings)
            {
                _context = context;
                _mapper = mapper;
                _emailSender = emailSender;
                _configurationSettings = configurationSettings; 
            }

        #region public

        public CustomerResponse GetAll(UserModelViewModel currentUser,
                                           int page = 1,
                                           int pageSize = 5,
                                           string sortColumn = "",
                                           string sortDirection = "ascending",
                                           string searchText = "")
            {
               /* var customerList = _context.Customers.ToList();
                return _mapper.Map<List<CustomerResponse>>(customerList);
               */

            var queryRes = _context.Customers
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

            /*
            var Address = res.Data
                             .Select(a => a.AddressId)
                             .Distinct()
                             .ToList();

            var users = _context.Addresses
                                 .Where(a => Address.Contains(a.Id))
                                 .ToDictionary(a => a.Id, x => _mapper.Map<AddressResult>(x));
            */
            var data = new CustomerResponse
            {
                customers = _mapper.Map<PagedResult<CustomerResult>>(res),
                //address = users
            };

            data.customers.Sortable.Add("Title", "Title");
            data.customers.Sortable.Add("CreatedDate", "Created Date");

            return data;

        }

        public CustomerLoginResponseViewModel Rigester(UserModelViewModel currentUser,CustomerRegisterViewModel vm)
        {
            if (_context.Customers
                              .Any(a => a.Email.ToLower().Equals(vm.Email.ToLower())))
            {
                throw new ServiceValidationException("Customer already exist");
            }

            var hashedPassword = HashPassword(vm.Password);

            var cretorId = 0;
            if (currentUser != null)
            {
                cretorId = currentUser.Id;
            }

            var customer = _context.Customers.Add(new Customer
            {
                Name = vm.Name,
                UserName = vm.UserName,
                Email = vm.Email.ToLower(),
                Password = hashedPassword,
                ConfirmPassword = hashedPassword,
                AddressId = 1,
                Phone = "",
                RoleId = 1,
                CreatedBy = cretorId,
                CreatedDate = DateTime.Now,
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
            res.Token = $"Bearer {GenerateJwtTaken(customer)}";

            return res;
        }

        public CustomerResult Confirmation(UserModelViewModel currentUser, string ConfirmationLink)
        {
            var customer = _context.Customers
                           .FirstOrDefault(a => a.ConfirmationLink
                                                    .Equals(ConfirmationLink)
                                                && !a.EmailConfirmed )
                       ?? throw new ServiceValidationException("Invalid or expired confirmation link received");

            customer.EmailConfirmed = true;
            customer.ConfirmationLink = string.Empty;
            _context.SaveChanges();
            return _mapper.Map<CustomerResult>(customer);
        }


        public CustomerLoginResponseViewModel Login(CustomerLoginViewModel vm)
        {

            var customer = _context.Customers.FirstOrDefault(x => x.Email.ToLower().Equals(vm.Email.ToLower()));

            if (customer == null || !VerifyHashPassword(vm.Password, customer.Password))
            {
                throw new ServiceValidationException(300, "User Is not valid Name or password");
            }

            var res = _mapper.Map<CustomerLoginResponseViewModel>(customer);
            res.Token = $"Bearer {GenerateJwtTaken(customer)}";

            return res;
        }


        public CustomerUpdateModelView UpdateProfile(UserModelViewModel currentUser, CustomerUpdateModelView request)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == currentUser.Id)
                ?? throw new ServiceValidationException("User not found");

            if (customer == null)
            {
                throw new ServiceValidationException("User not found");
            }

            customer.UserName = request.UserName;
            customer.UpdatedDate = DateTime.Now;
            customer.UpdatedBy = currentUser.Id;
            customer.Email = request.Email;
            customer.Phone = request.Phone;


            _context.SaveChanges();
            return _mapper.Map<CustomerUpdateModelView>(customer);
        }


        public void DeleteCustomer(UserModelViewModel currentUser, int id)
        {
            var user = new Customer();

            if (currentUser.IsSuperAdmin) {
                   user = _context.Customers
                                            .FirstOrDefault(x => x.Id == id)
                                             ?? throw new ServiceValidationException("User not found");
            }
            else
            {
                throw new ServiceValidationException("you have no access to delete your self");
            }
            

            user.Archived = 1;
            _context.SaveChanges();
        }


        public CustomerResult GetCustomer(int id)
        {
            var res = _context.Customers
                                   .FirstOrDefault(a => a.Id == id)
                                    ?? throw new ServiceValidationException("Invalid User id received");

            return _mapper.Map<CustomerResult>(res);
        }


        public AddressResult PutAddress(UserModelViewModel currentUser,
                                        AddressResult request)
        {

            var customer = new Customer();
            if (currentUser.IsSuperAdmin)
                { 
                     customer = _context.Customers.FirstOrDefault(x => x.Id == request.UserId)
                                 ?? throw new ServiceValidationException("Customer not found");

                if (customer == null)
                {
                    throw new ServiceValidationException("User not found");
                }
            }

            else {
                 customer = _context.Customers.FirstOrDefault(x => x.Id == currentUser.Id)
                             ?? throw new ServiceValidationException("Customer not found");
            }

            Address item = null;

            if (request.Id > 0)
            {
                item = _context.Addresses
                                .FirstOrDefault(a => a.Id == request.Id)
                                 ?? throw new ServiceValidationException("Invalid Address id received");


                item.City = request.City;
                item.Country = request.Country;
                item.Road = request.Road;
                item.ExtraInformation = request.ExtraInformation;

            }
            else
            {

                item = _context.Addresses.Add(new Address
                {
                    City = request.City,
                    Country = request.Country,
                    Road = request.Road,
                    ExtraInformation = request.ExtraInformation

                }).Entity;

                _context.SaveChanges();

            }

            customer.AddressId = item.Id;
            customer.Address = item;
            customer.UpdatedDate = DateTime.UtcNow;
            customer.UpdatedBy = currentUser.Id;

            _context.SaveChanges();
            return _mapper.Map<AddressResult>(item);
        }


        public CustomerResult ViewProfile(UserModelViewModel currentUser, int id)
        {
            var customer = new Customer();
            if (currentUser.IsSuperAdmin) { 
               customer = _context.Customers.FirstOrDefault(x => x.Id == id)
                    ?? throw new ServiceValidationException("User not found");

                if (customer == null)
                {
                    throw new ServiceValidationException("User not found");
                }
            }
            else { 
                customer = _context.Customers.FirstOrDefault(x => x.Id == currentUser.Id)
                    ?? throw new ServiceValidationException("User not found");
            }
            return _mapper.Map<CustomerResult>(customer);   
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

        private string GenerateJwtTaken(Customer user)
        {
            var jwtKey = "#test.key*j;ljklkjhadfsd";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , $"{user.UserName}"),
                new Claim(JwtRegisteredClaimNames.Email , user.Email ),
                new Claim("Id" , user.Id.ToString() ),
                new Claim("FirstName", user.Name),
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

        

        #endregion private
    }
}
