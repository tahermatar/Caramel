using Caramel.Attributes;
using Caramel.Core.Mangers.CustomerManger;
using Caramel.Core.Mangers.MealManager;
using Caramel.ModelViews;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Meal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caramel.Controllers
{

    [ApiController]
    public class MealController : ApiBaseController
    {
        private readonly IMealManager _mealManager;

        public MealController(IMealManager mealManager)
        {
            _mealManager = mealManager;
        }


        [HttpGet]
        [Route("api/Meal/GetResturantAllMeal")]
        [AllowAnonymous]
        public IActionResult GetResturantAllMeal( int ResturantId, MealCategoryEnum MealCat = MealCategoryEnum.All,
                                                  int page = 1,
                                                  int pageSize = 5,
                                                  string sortColumn = "",
                                                  string sortDirection = "ascending",
                                                  string searchText = "")
        {
            return Ok(_mealManager.GetResturantAllMeal(LoggedInUser, 
                                                       ResturantId,
                                                       MealCat, 
                                                       page,
                                                       pageSize,
                                                       sortColumn,
                                                       sortDirection,
                                                       searchText));
        }


        [Route("api/Meal/PutMeal")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Put_Resturant_Meal")]
        public IActionResult PutMeal(MealCreateModelView itemRequest)
        {
            var result = _mealManager.PutMeal(LoggedInUser, itemRequest);
            return Ok(result);
        }


        [HttpGet]
        [Route("api/Meal/viewMeal")]
        [AllowAnonymous]
        public IActionResult ViewProfile(int id)
        {
            var res = _mealManager.viewMeal(id);
            return Ok(res);
        }


        [HttpDelete]
        [Route("api/Meal/Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Delete_Meal")]
        public IActionResult Delete(int id)
        {
            return Ok(_mealManager.DeleteMeal(LoggedInUser, id));
        }
    }
}
