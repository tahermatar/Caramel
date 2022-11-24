using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Role
{
    public class PermissionCreateViewModel
    {
        public int PId { get; set; }
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
