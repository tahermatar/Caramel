using System;

#nullable disable

namespace Caramel.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Image { get; set; }
        public int AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool Archived { get; set; }
        public string Phone { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ConfirmationLink { get; set; }


        public virtual Address Address { get; set; }
        public int RoleId { get; set; }

    }
}
