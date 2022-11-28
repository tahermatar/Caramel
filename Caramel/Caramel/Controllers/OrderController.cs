using Caramel.Core.Mangers.OrderManager;
using Caramel.ModelViews.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caramel.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : ApiBaseController
    {

        private readonly IOrderManager _orderManager;
        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpGet]
        public IActionResult ViewMealOrders()
        {
            var res = _orderManager.ViewMealOrder(LoggedInUser);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateMealOrder(CreateOrderViewModel vm)
        {
            var res = _orderManager.CreateMealOrder(LoggedInUser, vm);
            return Ok(res);
        }
        [HttpPut]
        public IActionResult FinishMealOrder( int id)
        {
            _orderManager.FinishMealOrder(LoggedInUser, id);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteMealOrder(int id)
        {
            _orderManager.DeleteMealOrder(LoggedInUser,id);
            return Ok("Delete success");
        }



    }
}
