using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Module
    {
        public Module()
        {
            Permissions = new HashSet<Permission>();
        }

        public int MId { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
        public short Archived { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
