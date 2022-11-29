using Caramel.Attributes;
using Caramel.Core.Mangers.CustomerManger;
using Caramel.Core.Mangers.UserManger;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Caramel.Controllers
{
    [ApiController]
    public class CustomerController : ApiBaseController
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }


        // GET: api/<CustomerController>
        [HttpGet]
        [Route("api/Customer/GetAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_All_Customer")]
        public IActionResult GetAll(int page = 1,
                                      int pageSize = 5,
                                      string sortColumn = "",
                                      string sortDirection = "ascending",
                                      string searchText = "")
        {
            return Ok(_customerManager.GetAll(LoggedInUser, page, pageSize,
                                               sortColumn,
                                               sortDirection,
                                               searchText));
        }


        // POST api/<CustomerController>
        [HttpPost]
        [Route("api/Customer/Create")]
        [AllowAnonymous]
        public IActionResult Rigester([FromBody] CustomerRegisterViewModel vm)
        {
            var res = _customerManager.Rigester(LoggedInUser,vm);
            return Ok(res);
        }



        [HttpDelete]
        [Route("api/Customer/Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Delete_Customer")]
        public IActionResult Delete( int id)
        {
            _customerManager.DeleteCustomer(LoggedInUser, id);
            return Ok();
        }


        [Route("api/Customer/Confirmation")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Confirmation_Customer")]
        public IActionResult Confirmation(string confirmationLink)
        {
            var result = _customerManager.Confirmation(confirmationLink);
            return Ok(result);
        }


        [Route("api/Customer/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] CustomerLoginViewModel vm)
        {
            var res = _customerManager.Login(vm);
            return Ok(res);
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


        [HttpGet]
        [Route("api/Customer/viewProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_Profile")]
        public IActionResult ViewProfile( int id)
        {
            var res = _customerManager.ViewProfile(LoggedInUser,id);
            return Ok(res);
        }
    }
}
