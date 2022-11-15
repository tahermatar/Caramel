using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Rolepermission
    {
        public int RpId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
        public short Archived { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
