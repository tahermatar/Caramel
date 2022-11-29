using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Role
{
    public class RolePermissionViewModel
    {
        public int RpId { get; set; }
        public int RoleId { get; set; }
        public List<int> PermissionId { get; set; }
    }
}
