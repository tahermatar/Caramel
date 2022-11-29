using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Userrole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
        public bool Archived { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
