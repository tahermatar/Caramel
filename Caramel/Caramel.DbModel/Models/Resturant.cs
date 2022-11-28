using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Resturant
    {
        public Resturant()
        {
            Meals = new HashSet<Meal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public double TotalRate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool Archived { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? MealId { get; set; }
        public int? ServiceCategoryId { get; set; }
        public string WorkingTime { get; set; }
        public int? OrderId { get; set; }
        public int IsChef { get; set; }
        public int? RateId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Rate Rate { get; set; }
        public virtual ServiceCategory ServiceCategory { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }



        public bool EmailConfirmed { get; set; }
        public string ConfirmationLink { get; set; }
        public int RoleId { get; set; }

    }
}
