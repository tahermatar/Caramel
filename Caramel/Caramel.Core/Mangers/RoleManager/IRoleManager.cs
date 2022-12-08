using Caramel.Models;
using Caramel.ModelViews.Role;
using Caramel.ModelViews.User;
using System.Collections.Generic;

namespace Caramel.Core.Mangers.RoleManger
{
    public interface IRoleManager
    {
        Module PutModule(UserModelViewModel currentUser, ModuleCreateViewModel vm);
        List<Module> GetModule();
        //Module UpdateModule(ModuleCreateViewModel vm);
        void DeleteModule(int id);

        Permission CreatePermission(PermissionCreateViewModel vm);
        List<Permission> GetPermission();
        Permission UpdatePermission(PermissionCreateViewModel vm);
        void DeletePermission(int id);

        Role CreateRole(RoleCreateViewModel vm);
        List<Role> GetRole();
        Role UpdateRole(RoleCreateViewModel vm);
        void DeleteRole(int id);

        List<Rolepermission> CreateRolePermition(RolePermissionViewModel vm);
        List<Rolepermission> GetRolePermision();
        List<Rolepermission> GetRolePermision(int RoleId);
        List<Rolepermission> UpdateRolePermition(RolePermissionViewModel vm);
        void DeleteRolePermition(int id);


        Userrole CreateUserRole(UserRoleModelView vm);
        List<Userrole> GetUserRole();
        List<Userrole> GetUserRoleId(int UserId);
        Userrole UpdateUserRole(UserRoleModelView vm);
        void DeleteUserRole(int id);


        bool CheckAccess(UserModelViewModel userModel, List<string> permissions);

    }
}
