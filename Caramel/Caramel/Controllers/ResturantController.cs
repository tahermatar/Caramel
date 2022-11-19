using Caramel.Core.Mangers.ResturantManager;
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
    }
}
