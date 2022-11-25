using Caramel.Core.Mangers.CustomerManger;
using Caramel.Core.Mangers.UserManger;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.User;
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
        [Authorize]
        public IActionResult GetAll()
        {
            return Ok(_customerManager.GetAll());
        }

        // GET api/<CustomerController>/5
        [HttpGet]
        [Route("api/Customer/Get")]

        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerController>
        [HttpPost]
        [Route("api/Customer/Create")]

        public IActionResult Rigester([FromBody] CustomerRegisterViewModel vm)
        {
            var res = _customerManager.Rigester(vm);
            return Ok(res);
        }

        // PUT api/<CustomerController>/5
        [HttpPut]
        [Route("api/Customer/Update")]

        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete]
        [Route("api/Customer/Delete")]

        public void Delete(int id)
        {
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
    }
}
