using Caramel.Attributes;
using Caramel.Core.Mangers.RateManager.cs;
using Caramel.Core.Mangers.UserManger;
using Caramel.ModelViews.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caramel.Controllers
{
    [ApiController]
    public class UserController : ApiBaseController
    {
        private readonly IUserManager _userManager;
        private readonly IRateManager _rateManager;

        public UserController(IUserManager userManager, IRateManager rateManager)
        {
            _userManager = userManager;
            _rateManager = rateManager;
        }

        [HttpPost]
        [Route("api/User/SignUp")]
        public IActionResult SignUp([FromBody] UserRegisterViewModel vm)
        {
            var res = _userManager.SignUp(vm);
            return Ok(res);
        }

        [HttpPost]
        [Route("api/User/Login")]
        public IActionResult Login([FromBody] UserLoginViewModel vm)
        {
            var res = _userManager.Login(vm);
            return Ok(res);

        }

        [HttpPut]
        [Route("api/User/UpdateMyProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateMyProfile([FromBody] UserModelViewModel vm)
        {
            var user = _userManager.UpdateProfile(LoggedInUser, vm);
            return Ok(user);
        }


        [HttpGet]
        [Route("api/User/ViewResturantRating")]
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


        [HttpDelete]
        [Route("api/User/Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int id)
        {
            _userManager.DeleteUser(LoggedInUser, id);
            return Ok("Delete Success");
        }
    }
}
