using Caramel.Core.Mangers.ResturantManager;
using Caramel.ModelViews.Order;
using Caramel.ModelViews.Resturant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Caramel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResturantController : ApiBaseController
    {
        private IResturantManager _resturantManager;

        public ResturantController(IResturantManager resturantManager)
        {
            _resturantManager = resturantManager;
        }

        [Route("api/resturant/signUp")]
        [HttpPost]
        public IActionResult SignUp([FromBody] ResturantRegisterViewModel restReg)
        {
            var res = _resturantManager.SignUp(restReg);
            return Ok(res);
        }

        [Route("api/resturant/Login")]
        [HttpPost]
        public IActionResult Login([FromBody] ResturantLoginModelView restLogin)
        {
            var res = _resturantManager.Login(restLogin);
            return Ok(res);
        }

        [Route("api/resturant/Delete")]
        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _resturantManager.DeleteResturant(LoggedInResturant, id);
            return Ok();
        }

        [Route("api/resturant/fileretrive/profilepic")]
        [HttpGet]
        public IActionResult Retrive(string filename)
        {
            var folderPath = Directory.GetCurrentDirectory();
            folderPath = $@"{folderPath}\{filename}";
            var byteArray = System.IO.File.ReadAllBytes(folderPath);
            return File(byteArray, "image/jpeg", filename);
        }

        [Route("api/resturant/Update")]
        [HttpPut]
        [Authorize]
        public IActionResult UpdateMyProfile(ResturantModelView request)
        {
            var user = _resturantManager.UpdateProfile(LoggedInResturant, request);
            return Ok(user);
        }

        [Route("api/resturant/Confirmation")]
        [HttpPost]
        public IActionResult Confirmation(string confirmationLink)
        {
            var result = _resturantManager.Confirmation(confirmationLink);
            return Ok(result);
        }

        [Route("api/resturant/PutServiceCategory")]
        [HttpPost]
        [Authorize]
        public IActionResult PutServiceCategory(ServiceCategoryRequest serviceCategoryRequest)
        {
            var result = _resturantManager.PutServiceCategory(LoggedInResturant, serviceCategoryRequest);
            return Ok(result);
        }
        
        [Route("api/resturant/PutMealCategory")]
        [HttpPost]
        [Authorize]
        public IActionResult PutMealCategory(CategoryRequest categoryRequest)
        {
            var result = _resturantManager.PutMealCategory(LoggedInResturant, categoryRequest);
            return Ok(result);
        }

        [Route("api/resturant/PutMeal")]
        [HttpPost]
        [Authorize]
        public IActionResult PutMeal(MealRequest mealRequest)
        {
            var result = _resturantManager.PutMeal(LoggedInResturant, mealRequest);
            return Ok(result);
        }

        [Route("api/resturant/PutImage")]
        [HttpPost]
        [Authorize]
        public IActionResult PutImage(ImageRequest imageRequest)
        {
            var result = _resturantManager.PutImage(LoggedInResturant, imageRequest);
            return Ok(result);
        }
    }
}
