using Caramel.Attributes;
using Caramel.Core.Mangers.OrderManager;
using Caramel.ModelViews.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caramel.Controllers
{

    [ApiController]
    [Authorize]
    public class OrderController : ApiBaseController
    {

        private readonly IOrderManager _orderManager;
        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }


        [HttpPost]
        [Route("api/Order/CreateMealOrder")]
        [CaramelAuthrize(Permissions = "Create_Meal_Order")]
        public IActionResult CreateMealOrder(CreateOrderViewModel vm)
        {
            var res = _orderManager.CreateMealOrder(LoggedInUser, vm);
            return Ok(res);
        }


        [HttpGet]
        [Route("api/Order/ViewMealOrders")]
        public IActionResult ViewMealOrders()
        {
            var res = _orderManager.ViewMealOrder(LoggedInUser);
            return Ok(res);
        }


        [HttpPut]
        [Route("api/Order/FinishMealOrder")]
        [CaramelAuthrize(Permissions = "Finish_Meal_Order")]
        public IActionResult FinishMealOrder(int id)
        {
            _orderManager.FinishMealOrder(LoggedInUser, id);
            return Ok();
        }


        [HttpDelete]
        [Route("api/Order/DeleteMealOrder")]
        [CaramelAuthrize(Permissions = "Delete_Meal_Order")]
        public IActionResult DeleteMealOrder(int id)
        {
            _orderManager.DeleteMealOrder(LoggedInUser,id);
            return Ok("Delete Success");
        }
    }
}
