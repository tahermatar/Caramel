using Caramel.Models;
using Caramel.ModelViews.Role;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.RoleManger
{
    public interface IRoleManager
    {

        List<Module> GetModule();
        Module CreateModule(ModuleCreateViewModel vm);
        Module UpdateModule(ModuleCreateViewModel vm);
        void DeleteModule(int id);


        List<Permission> GetPermission();
        Permission CreatePermission(PermissionCreateViewModel vm);
        Permission UpdatePermission(PermissionCreateViewModel vm);
        void DeletePermission(int id);


        List<Role> GetRole();
        Role CreateRole(RoleCreateViewModel vm);
        Role UpdateRole(RoleCreateViewModel vm);
        void DeleteRole(int id);


        List<Rolepermission> GetRolePermision();
        List<Rolepermission> GetRolePermision(int RoleId);
        List<Rolepermission> CreateRolePermition(RolePermissionViewModel vm);
        List<Rolepermission> UpdateRolePermition(RolePermissionViewModel vm);
        void DeleteRolePermition(int id);


        List<Userrole> GetUserRole();
        List<Userrole> GetUserRoleId(int UserId);
        Userrole CreateUserRole(UserRoleModelView vm);
        Userrole UpdateUserRole(UserRoleModelView vm);
        void DeleteUserRole(int id);


        bool CheckAccess(UserModelViewModel userModel, List<string> permissions);

    }
}
