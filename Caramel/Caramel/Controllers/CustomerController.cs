using Caramel.Attributes;
using Caramel.Core.Mangers.CustomerManger;
using Caramel.Core.Mangers.RateManager.cs;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Rate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Caramel.Controllers
{
    [ApiController]
    public class CustomerController : ApiBaseController
    {
        private readonly ICustomerManager _customerManager;
        private readonly IRateManager _rateManager;

        public CustomerController(ICustomerManager customerManager, IRateManager rateManager)
        {
            _customerManager = customerManager;
            _rateManager = rateManager;
        }


        [HttpPost]
        [Route("api/Customer/SignUp")]
        public IActionResult SignUp([FromBody] CustomerRegisterViewModel vm)
        {
            var res = _customerManager.Rigester(LoggedInUser, vm);
            return Ok(res);
        }


        [Route("api/Customer/Confirmation")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Confirmation_Customer")]
        public IActionResult Confirmation(string confirmationLink)
        {
            var result = _customerManager.Confirmation(LoggedInUser, confirmationLink);
            return Ok(result);
        }


        [Route("api/Customer/Login")]
        [HttpPost]
        public IActionResult Login([FromBody] CustomerLoginViewModel vm)
        {
            var res = _customerManager.Login(vm);
            return Ok(res);
        }


        [HttpPost]
        [Route("api/Customer/PutRate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Add_Rate")]
        public IActionResult PutRate([FromBody] AddRateModelView addRate)
        {
            var res = _rateManager.PutRate(LoggedInUser, addRate);
            return Ok(res);
        }


        [HttpGet]
        [Route("api/Customer/ViewProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_Profile")]
        public IActionResult ViewProfile()
        {
            var res = _customerManager.ViewProfile(LoggedInUser);
            return Ok(res);
        }


        [HttpGet]
        [Route("api/Customer/GetCustomer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_Customer")]
        public IActionResult GetCustomer(int id)
        {
            var res = _customerManager.GetCustomer(id);
            return Ok(res);
        }


        [HttpGet]
        [Route("api/Customer/GetAllCustomer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_All_Customer")]
        public IActionResult GetAllCustomer(int page = 1,
                                            int pageSize = 5,
                                            string sortColumn = "",
                                            string sortDirection = "ascending",
                                            string searchText = "")
        {
            var res = _customerManager.GetAllCustomer(LoggedInUser,
                                                      page,
                                                      pageSize,
                                                      sortColumn,
                                                      sortDirection,
                                                      searchText);
            return Ok(res);
        }


        [HttpGet]
        [Route("api/Customer/ViewResturantRating")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_Resturant_Rating")]
        public IActionResult ViewResturantRating(int page = 1,
                                              int pageSize = 5,
                                              string sortColumn = "",
                                              string sortDirection = "ascending",
                                              string searchText = "")
        {
            var res = _rateManager.ViewResturantRate(LoggedInUser,
                                                    page,
                                                    pageSize,
                                                    sortColumn,
                                                    sortDirection,
                                                    searchText);
            return Ok(res);
        }


        [HttpGet]
        [Route("api/Customer/ViewCustomerRate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_Customer_Rate")]
        public IActionResult ViewCustomerRate(int page = 1,
                                              int pageSize = 5,
                                              string sortColumn = "",
                                              string sortDirection = "ascending",
                                              string searchText = "")
        {
            var res = _rateManager.ViewCustomerRate(LoggedInUser,
                                                    page,
                                                    pageSize,
                                                    sortColumn,
                                                    sortDirection,
                                                    searchText);
            return Ok(res);
        }


        [Route("api/Customer/fileretrive/Profilepic")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Retrive(string filename)
        {
            var folderPath = Directory.GetCurrentDirectory();
            folderPath = $@"{folderPath}\{filename}";
            var byteArray = System.IO.File.ReadAllBytes(folderPath);
            return File(byteArray, "image/jpeg", filename);
        }


        [Route("api/Customer/UpdateProfile")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Update_Profile")]
        public IActionResult UpdateProfile([FromBody] CustomerUpdateModelView vm)
        {
            var user = _customerManager.UpdateProfile(LoggedInUser, vm);
            return Ok(user);
        }


        [Route("api/Customer/PutAddress")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Update_Customer_Address")]
        public IActionResult PutAddress(AddressResult itemRequest)
        {
            var result = _customerManager.PutAddress(LoggedInUser, itemRequest);
            return Ok(result);
        }


        [HttpDelete]
        [Route("api/Customer/DeleteRate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Delete_Rating")]
        public IActionResult DeleteRate(int id)
        {
            _rateManager.DeleteRate(LoggedInUser, id);
            return Ok("Delete Success");
        }


        [HttpDelete]
        [Route("api/Customer/DeleteCustomer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Delete_Customer")]
        public IActionResult Delete( int id)
        {
            _customerManager.DeleteCustomer(LoggedInUser, id);
            return Ok("Delete Success");
        }
    }
}
