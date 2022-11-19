using Caramel.Core.Mangers.CustomerManger;
using Caramel.Core.Mangers.UserManger;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Caramel.Controllers
{
    [ApiController]
    public class CustomerController : ApiBaseController
    {
        private readonly ICustomerManger _customerManager;

        public CustomerController(ICustomerManger customerManager)
        {
            _customerManager = customerManager;
        }


        // GET: api/<CustomerController>
        [HttpGet]
        [Route("api/Customer/GetAll")]
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
    }
}
