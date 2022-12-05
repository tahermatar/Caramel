using Caramel.Attributes;
using Caramel.Core.Mangers.RateManager.cs;
using Caramel.Core.Mangers.ResturantManager;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly IRateManager _rateManager;
        public ResturantController(IResturantManager resturantManager, IRateManager rateManager)
        {
            _resturantManager = resturantManager;
            _rateManager = rateManager;
        }

        [Route("api/resturant/SignUp")]
        [HttpPost]
        public IActionResult SignUp([FromBody] ResturantRegisterViewModel restReg)
        {
            var res = _resturantManager.SignUp(LoggedInUser, restReg);
            return Ok(res);
        }


        [Route("api/Resturant/Confirmation")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Confirmation_Resturant")]
        public IActionResult Confirmation(string confirmationLink)
        {
            var result = _resturantManager.Confirmation(LoggedInUser, confirmationLink);
            return Ok(result);
        }


        [Route("api/resturant/Login")]
        [HttpPost]
        public IActionResult Login([FromBody] ResturantLoginModelView restLogin)
        {
            var res = _resturantManager.Login(restLogin);
            return Ok(res);
        }


        [Route("api/Resturant/UpdateProfile")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Update_Resturant_Profile")]
        public IActionResult UpdateProfile([FromBody] ResturantModelView vm)
        {
            var user = _resturantManager.UpdateProfile(LoggedInUser, vm);
            return Ok(user);
        }


        [Route("api/Resturant/UpdateRegistrationData")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Update_Registration_Data")]
        public IActionResult EditRegistrationData([FromBody] ResturantRegViewModel reg)
        {
            var user = _resturantManager.UpdateRegistrationData(LoggedInUser, reg);
            return Ok(user);
        }


        [Route("api/Resturant/UpdateResturantAddress")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Update_Resturant_Address")]
        public IActionResult UpdateResturantAddress([FromBody] AddressResult reg)
        {
            var user = _resturantManager.UpdateResturantAddress(LoggedInUser, reg);
            return Ok(user);
        }


        [Route("api/resturant/fileretrive/Profilepic")]
        [HttpGet]
        public IActionResult Retrive(string filename)
        {
            var folderPath = Directory.GetCurrentDirectory();
            folderPath = $@"{folderPath}\{filename}";
            var byteArray = System.IO.File.ReadAllBytes(folderPath);
            return File(byteArray, "image/jpeg", filename);
        }


        [Route("api/resturant/LogOut")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult LogOut()
        {
            var res = HttpContext.SignOutAsync();
            return Ok(res);
        }


        [HttpGet]
        [Route("api/Resturant/ViewProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_Resturant_Profile")]
        public IActionResult ViewProfile(int id)
        {
            var res = _resturantManager.ViewProfile(LoggedInUser, id);
            return Ok(res);
        }


        [HttpGet]
        [Route("api/Resturant/GetAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_All_Resturant")]
        public IActionResult GetAll(int page = 1,
                                      int pageSize = 5,
                                      string sortColumn = "",
                                      string sortDirection = "ascending",
                                      string searchText = "")
        {
            return Ok(_resturantManager.GetAll(LoggedInUser, page, pageSize,
                                               sortColumn,
                                               sortDirection,
                                               searchText));
        }


        [HttpGet]
        [Route("api/Resturant/ViewResturantRate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "View_All_Rating")]
        public IActionResult ViewResturantRate(int page = 1,
                                               int pageSize = 5,
                                               string sortColumn = "",
                                               string sortDirection = "ascending",
                                               string searchText = "")
        {
            return Ok(_rateManager.ViewResturantRate(LoggedInUser,
                                                    page,
                                                    pageSize,
                                                    sortColumn,
                                                    sortDirection,
                                                    searchText));
        }


        [HttpDelete]
        [Route("api/Resturant/Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CaramelAuthrize(Permissions = "Delete_Resturant")]
        public IActionResult Delete(int id)
        {
            _resturantManager.DeleteResturant(LoggedInUser, id);
            return Ok("Delete Success");
        }
    }
}
