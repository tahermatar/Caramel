using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Rolepermissions = new HashSet<Rolepermission>();
        }

        public int PId { get; set; }
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
        public bool Archived { get; set; }

        public virtual Module Module { get; set; }
        public virtual ICollection<Rolepermission> Rolepermissions { get; set; }
    }
}
