using Caramel.Attributes;
using Caramel.Core.Mangers.RoleManger;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews.Role;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Caramel.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoleController : ApiBaseController
    {
        private readonly IRoleManager _roleManger;
        public RoleController(IRoleManager roleManger)
        {
            _roleManger = roleManger;
        }


        // GET: api/<RoleController>
        [HttpGet]
        [CaramelAuthrize(Permissions = "sup2")]
        public IActionResult GetModule()
        {
            return Ok(_roleManger.GetModule());
        }


        // POST api/<RoleController>
        [HttpPost]
        public IActionResult CreateModule([FromBody]ModuleCreateViewModel vm)
        {
            return Ok(_roleManger.CreateModule(vm));
        }


        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ModuleCreateViewModel vm)
        {
            var res = _roleManger.UpdateModule(vm);
            return Ok(res);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _roleManger.DeleteModule(id);
            return Ok("Delete Success");
        }



        /////////////////
        ///

        // GET: api/<RoleController>
        [HttpGet]
        public IActionResult GetPermission()
        {

            return Ok(_roleManger.GetPermission());
        }


        // POST api/<RoleController>
        [HttpPost]
        public IActionResult CreatePermission([FromBody] PermissionCreateViewModel vm)
        {
            return Ok(_roleManger.CreatePermission(vm));
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public IActionResult UpdatePermission([FromBody] PermissionCreateViewModel vm)
        {
            var res = _roleManger.UpdatePermission(vm);
            return Ok(res);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public IActionResult DeletePermission(int id)
        {
            _roleManger.DeletePermission(id);
            return Ok("Delete Success");
        }




        /////////////////
        ///

        // GET: api/<RoleController>
        [HttpGet]
        public IActionResult GetRole()
        {

            return Ok(_roleManger.GetRole());
        }


        // POST api/<RoleController>
        [HttpPost]
        public IActionResult CreateRole([FromBody] RoleCreateViewModel vm)
        {
            return Ok(_roleManger.CreateRole(vm));
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateRole([FromBody] RoleCreateViewModel vm)
        {
            var res = _roleManger.UpdateRole(vm);
            return Ok(res);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            _roleManger.DeletePermission(id);
            return Ok("Delete Success");
        }



        /////////////////
        ///

        // GET: api/<RoleController>
        [HttpGet]
        public IActionResult GetRolePermision()
        {

            return Ok(_roleManger.GetRolePermision());
        }

        [HttpGet]
        public IActionResult GetRolePermisionId(int RoleId)
        {
            return Ok(_roleManger.GetRolePermision(RoleId));
        }


        // POST api/<RoleController>
        [HttpPost]
        public IActionResult CreateRolePermition([FromBody] RolePermissionViewModel vm)
        {
            return Ok(_roleManger.CreateRolePermition(vm));
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateRolePermition([FromBody] RolePermissionViewModel vm)
        {
            var res = _roleManger.UpdateRolePermition(vm);
            return Ok(res);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRolePermition(int id)
        {
            _roleManger.DeleteRolePermition(id);
            return Ok("Delete Success");
        }



        /////////////////
        ///

        // GET: api/<RoleController>
        [HttpGet]
        public IActionResult GetUserRole()
        {

            return Ok(_roleManger.GetUserRole());
        }

        [HttpGet]
        public IActionResult GetUserRoleId(int UserId)
        {
            return Ok(_roleManger.GetUserRoleId(UserId));
        }


        // POST api/<RoleController>
        [HttpPost]
        public IActionResult CreateUserRole([FromBody] UserRoleModelView vm)
        {
            return Ok(_roleManger.CreateUserRole(vm));
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUserRole([FromBody] UserRoleModelView vm)
        {
            var res = _roleManger.UpdateUserRole(vm);
            return Ok(res);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUserRole(int id)
        {
            _roleManger.DeleteUserRole(id);
            return Ok("Delete Success");
        }
    }
}
