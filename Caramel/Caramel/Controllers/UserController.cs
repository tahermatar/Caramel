using Caramel.Core.Mangers.UserManger;
using Caramel.ModelViews.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Caramel.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private readonly IUserManger _userManager;

        public UserController(IUserManger userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Rigester([FromBody] UserRegisterViewModel vm)
        {
            var res = _userManager.Rigester(vm);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLoginViewModel vm)
        {
            var res = _userManager.Login(vm);
            return Ok(res);

        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateMyProfile([FromBody] UserModelViewModel vm)
        {
            var user = _userManager.UpdateProfile(LoggedInUser, vm);
            return Ok(user);
        }




        [HttpGet]
        [Authorize]
        public IActionResult Retrive(string filename)
        {
            var folderPath = Directory.GetCurrentDirectory();
            folderPath = $@"{folderPath}\{filename}";
            var byteArray = System.IO.File.ReadAllBytes(folderPath);
            return File(byteArray, "image/jpeg", filename);
        }
        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _userManager.DeleteUser(LoggedInUser, id);
            return Ok();
        }
    }
}
