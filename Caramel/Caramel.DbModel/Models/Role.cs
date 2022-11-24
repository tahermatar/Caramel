using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Role
    {
        public Role()
        {
            Rolepermissions = new HashSet<Rolepermission>();
            Userroles = new HashSet<Userrole>();
        }

        public int RId { get; set; }
        public string RoleName { get; set; }
        public DateTime? CreatedUtc { get; set; }
        public DateTime? LastUpdatedUtc { get; set; }
        public bool Archived { get; set; }

        public virtual ICollection<Rolepermission> Rolepermissions { get; set; }
        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
