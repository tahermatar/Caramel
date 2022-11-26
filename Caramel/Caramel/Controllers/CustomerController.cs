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
        [CaramelAuthrize(Permissions = "customer_all_view")]
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

        public IActionResult Rigester([FromBody] CustomerRegisterViewModel vm)
        {
            var res = _customerManager.Rigester(vm);
            return Ok(res);
        }



        [HttpDelete]
        [Route("api/Customer/Delete")]
        [Authorize]
        public IActionResult Delete( int id)
        {
            _customerManager.DeleteCustomer(LoggedInUser, id);
            return Ok();
        }


        [Route("api/Customer/Confirmation")]
        [HttpPost]
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
        [AllowAnonymous]
        public IActionResult UpdateMyProfile([FromBody] CustomerUpdateModelView vm)
        {
            var user = _customerManager.UpdateProfile(LoggedInCustomer, vm);
            return Ok(user);
        }

        [Route("api/Customer/PutAddress")]
        [HttpPut]
        [Authorize]
        public IActionResult PutAddress(AddressResult itemRequest)
        {
            var result = _customerManager.PutAddress(LoggedInCustomer, itemRequest);
            return Ok(result);
        }


        [HttpGet]
        [Route("api/Customer/viewProfile")]
        [Authorize]
        public IActionResult ViewProfile()
        {
            var res = _customerManager.ViewProfile(LoggedInCustomer);
            return Ok(res);
        }
    }
}
