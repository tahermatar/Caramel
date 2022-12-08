using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<Userrole>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsSuperAdmin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool Archived { get; set; }
        public virtual ICollection<Userrole> UserRoles { get; set; }
    }
}
