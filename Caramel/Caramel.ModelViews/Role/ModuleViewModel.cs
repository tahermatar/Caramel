using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Role
{
    public class ModuleViewModel
    {
        public int MId { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
        public bool Archived { get; set; }
    }
}
