using Caramel.Common.Extinsions;
using Caramel.Data;
using Caramel.Models;
using Caramel.ModelViews.Role;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Caramel.Core.Mangers.RoleManger
{
    public class RoleManager : IRoleManager
    {
        private readonly CaramelDbContext _context;

        public RoleManager(CaramelDbContext context)
        {
            _context = context;
        }

        //Module
        public Module CreateModule(UserModelViewModel currentUser, ModuleCreateViewModel vm)
        {
            //if (!currentUser.IsSuperAdmin)
            //{
            //    throw new ServiceValidationException(300, "You have no access to add or update module");
            //}

            var chick = _context.Modules.FirstOrDefault(x => x.Name == vm.Name 
                                                                    && x.Archived.Equals(false));

            if (chick != null)
            {
                throw new ServiceValidationException(300, "Module Already Exist");
            }
            var module = new Module
            {
                Name = vm.Name,
                Key = vm.Key,
                CreatedUtc = DateTime.Now,
                LastUpdatedUtc = DateTime.MinValue,
                Archived = true
            };

            _context.Modules.Add(module);
            _context.SaveChanges();

            return module;

            
            //Module module = null;

            //if (vm.MId > 0)
            //{
            //    module = _context.Modules.FirstOrDefault(x => x.MId == vm.MId && x.Archived.Equals(false))
            //                            ?? throw new ServiceValidationException(300, "Invalid module id received");

            //    module.Name = vm.Name;
            //    module.Key = vm.Key;
            //    module.LastUpdatedUtc = DateTime.Now;
            //    module.Archived = false;
            //}

            //else
            //{
            //    module = _context.Modules.FirstOrDefault(x => x.Name == vm.Name /*&& x.Archived.Equals(false)*/)
            //                            ?? throw new ServiceValidationException(300, "Module already exist");

            //    module = _context.Modules.Add(new Module
            //    {
            //        Name = vm.Name,
            //        Key = vm.Key,
            //        CreatedUtc = DateTime.Now,
            //        LastUpdatedUtc = DateTime.MinValue
            //    }).Entity;

            //    _context.SaveChanges();
            //}

            //_context.Modules.Add(module);
            //_context.SaveChanges();

            //return module;
        }

        public List<Module> GetModule()
        {
           var res = _context.Modules.Where(x => x.Archived == false).ToList();
            return res;
        }

        public Module UpdateModule(ModuleCreateViewModel vm)
        {
            var chick = _context.Modules.FirstOrDefault(x => x.MId == vm.MId)
           ?? throw new ServiceValidationException("Module not found");


            chick.Name = vm.Name;
            chick.Key = vm.Key;
            chick.LastUpdatedUtc = DateTime.Now;

            _context.SaveChanges();

            return chick;

        }
        public void DeleteModule(int id)
        {
            var chick = _context.Modules.FirstOrDefault( x => x.MId == id)
                ?? throw new ServiceValidationException("Module not found");
            chick.Archived = true;
            _context.SaveChanges();

        }


        //Permission
        public Permission CreatePermission(PermissionCreateViewModel vm)
        {
            var chick = _context.Permissions.FirstOrDefault(x => x.Title == vm.Title
                       && x.ModuleId.Equals(vm.ModuleId) && x.Archived.Equals(false));

            if (chick != null)
            {
                throw new ServiceValidationException(300, "Permissions Already Exist");
            }


            var Permissions = new Permission
            {
                ModuleId = vm.ModuleId,
                Title = vm.Title,
                Code = vm.Code,
                Description = vm.Description,
                CreatedUtc = DateTime.Now,
                Archived = true,

            };

            _context.Permissions.Add(Permissions);
            _context.SaveChanges();

            return Permissions;
        }

        public List<Permission> GetPermission()
        {
            var res = _context.Permissions.Where(x => x.Archived == false).ToList();
            return res;
        }

        public Permission UpdatePermission(PermissionCreateViewModel vm)
        {
            var chick = _context.Permissions.FirstOrDefault(x => x.PId == vm.PId)
                       ?? throw new ServiceValidationException("Permissions not found");


            chick.ModuleId = vm.ModuleId;
            chick.Title = vm.Title;
            chick.Code = vm.Code;
            chick.Description = vm.Description;
            chick.LastUpdatedUtc = DateTime.Now;

            _context.SaveChanges();

            return chick;
        }

        public void DeletePermission(int id)
        {
            var chick = _context.Permissions.FirstOrDefault(x => x.PId == id)
                ?? throw new ServiceValidationException("Permissions not found");
            chick.Archived = true;
            _context.SaveChanges();
        }


        //Role
        public Role CreateRole(RoleCreateViewModel vm)
        {
            var chick = _context.Roles.FirstOrDefault(x => x.RoleName == vm.RoleName
                                                                      && x.Archived.Equals(false));

            if (chick != null)
            {
                throw new ServiceValidationException(300, "Role Already Exist");
            }

            var Role = new Role
            {
                RoleName = vm.RoleName,
                CreatedUtc = DateTime.Now,
                Archived = false,

            };

            _context.Roles.Add(Role);
            _context.SaveChanges();

            return Role;
        }

        public List<Role> GetRole()
        {
            var res = _context.Roles.Where(x => x.Archived == false).ToList();
            return res;
        }

        public Role UpdateRole(RoleCreateViewModel vm)
        {
            var chick = _context.Roles.FirstOrDefault(x => x.RId == vm.RId)
                       ?? throw new ServiceValidationException("Role not found");

            chick.RoleName = vm.RoleName;
            chick.LastUpdatedUtc = DateTime.Now;

            _context.SaveChanges();

            return chick;
        }

        public void DeleteRole(int id)
        {
            var chick = _context.Roles.FirstOrDefault(x => x.RId == id)
                       ?? throw new ServiceValidationException("Role not found");
            chick.Archived = true;
            _context.SaveChanges();
        }


        //Role Permission
        public List<Rolepermission> CreateRolePermition(RolePermissionViewModel vm)
        {
            var chick = _context.Rolepermissions.FirstOrDefault(x => x.RoleId.Equals(vm.RoleId)
            && x.PermissionId.Equals(vm.PermissionId) && x.Archived.Equals(false));

            if (chick != null)
            {
                throw new ServiceValidationException(300, "Role Permition Already Exist");
            }
            List<Rolepermission> res = new List<Rolepermission>();

            foreach (var item in vm.PermissionId)
            {
                var rolePermission = new Rolepermission
                {
                    RoleId = vm.RoleId,
                    PermissionId = item,
                    CreatedUtc = DateTime.Now,
                    Archived = false
                };
                _context.Rolepermissions.Add(rolePermission);
                _context.SaveChanges();
                res.Add(rolePermission);

            }

            return res;
        }

        public List<Rolepermission> GetRolePermision()
        {
            var res = _context.Rolepermissions.Where(x => x.Archived == false).ToList();
            return res;
        }

        public List<Rolepermission> GetRolePermision(int RoleId)
        {
            var res = _context.Rolepermissions.Where(x => x.RoleId.Equals(RoleId)&& x.Archived.Equals(false)).ToList();
            return res;
        }

        public List<Rolepermission> UpdateRolePermition(RolePermissionViewModel vm)
        {
            var chick = _context.Rolepermissions.Where(x => x.RoleId.Equals(vm.RoleId) && x.Archived.Equals(false))
                      ?? throw new ServiceValidationException("Role Permissions not found");

            foreach (var item in chick)
            {
                _context.Rolepermissions.Remove(item);
            }

            var chickExsist = _context.Rolepermissions.FirstOrDefault(x => x.RoleId.Equals(vm.RoleId)
                              && x.PermissionId.Equals(vm.PermissionId) && x.Archived.Equals(false));
            if (chickExsist != null)
            {
                throw new ServiceValidationException(300, "Role Permition Already Exist");
            }

            List<Rolepermission> res = new List<Rolepermission>();

            foreach (var item in vm.PermissionId)
            {
                var rolePermission = new Rolepermission
                {
                    RoleId = vm.RoleId,
                    PermissionId = item,
                    CreatedUtc = DateTime.Now,
                    LastUpdatedUtc = DateTime.Now,
                    Archived = false
                };
                _context.Rolepermissions.Add(rolePermission);
                _context.SaveChanges();
                res.Add(rolePermission);
            }

            return res;
        }

        public void DeleteRolePermition(int id)
        {
            var chick = _context.Rolepermissions.Where(x => x.RpId.Equals(id))
                      ?? throw new ServiceValidationException("Role Permissions not found");

            foreach (var item in chick)
            {
                item.Archived = true;
                item.LastUpdatedUtc = DateTime.Now;
                _context.SaveChanges();
            }
        }


        //User Role
        public Userrole CreateUserRole(UserRoleModelView vm)
        {
            var chick = _context.Userroles.FirstOrDefault(x => x.UserId.Equals(vm.UserId) 
                                                            && x.RoleId.Equals(vm.RoleId) 
                                                            && x.Archived.Equals(false));

            if (chick != null)
            {
                throw new ServiceValidationException(300, "User Role Already Exist");
            }

            var Userroles = new Userrole
            {
                UserId = vm.UserId,
                RoleId = vm.RoleId,
                CreatedUtc = DateTime.Now,
                Archived = false,

            };

            _context.Userroles.Add(Userroles);
            _context.SaveChanges();

            return Userroles;
        }

        public List<Userrole> GetUserRole()
        {
            var res = _context.Userroles.Where(x => x.Archived == false).ToList();
            return res;
        }

        public List<Userrole> GetUserRoleId(int UserId)
        {
            var res = _context.Userroles.Where(x => x.UserId.Equals(UserId)
                                                 && x.Archived.Equals(false)).ToList();
            return res;
        }

        public Userrole UpdateUserRole(UserRoleModelView vm)
        {
            var chick = _context.Userroles.FirstOrDefault(x => x.UserId.Equals(vm.UserId) 
                                                           &&  x.RoleId.Equals(vm.RoleId))
                       ?? throw new ServiceValidationException("Role not found");

            chick.RoleId = vm.RoleId;
            chick.LastUpdatedUtc = DateTime.Now;

            _context.SaveChanges();

            return chick;
        }

        public void DeleteUserRole(int Id)
        {
            var chick = _context.Userroles.FirstOrDefault(x => x.Id == Id)
                      ?? throw new ServiceValidationException("Role not found");

            chick.Archived = true;
            _context.SaveChanges();
        }


        public bool CheckAccess(UserModelViewModel userModel, List<string> permissions)
        {
            if (userModel.Id > 1000 && userModel.Id < 10000) { 
             var p =  _context.Rolepermissions.Where(x => x.RoleId == 1).
                                              ToList();
                var r = new List<Permission>();

                foreach (var item in p) {
                   r.Add(_context.Permissions.FirstOrDefault(x => x.PId == item.PermissionId));
                }
             

                var ee = r.Any(x => permissions.Contains(x.Code));
                return ee;
            }
            else if (userModel.Id > 10000)
            {
                    var p = _context.Rolepermissions.Where(x => x.RoleId == 4).
                                                     ToList();
                    var r = new List<Permission>();

                    foreach (var item in p)
                    {
                        r.Add(_context.Permissions.FirstOrDefault(x => x.PId == item.PermissionId));
                    }


                    var ee = r.Any(x => permissions.Contains(x.Code));
                    return ee;
                }
            else { 
                    var userTest = _context.Users.FirstOrDefault(x => x.Id == userModel.Id)
                                 ?? throw new ServiceValidationException("Invalid User Id"); 

                    if (userTest.IsSuperAdmin == true)
                    {
                        return true;
                    }
            
            var userPermission = _context.Userpermissionviews.Where(x => x.UserId == userModel.Id).ToList();

            return userPermission.Any(x => permissions.Contains(x.Code));
            }
        }
    }
}
