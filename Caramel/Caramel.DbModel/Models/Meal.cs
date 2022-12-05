using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Meal
    {
        public Meal()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string MealName { get; set; }
        public int MealCategoryId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Component { get; set; }
        public int IsAvailable { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool Archived { get; set; }
        public int ServiceCategoryId { get; set; }
        public int? ResturantId { get; set; }
        public string Image { get; set; }
        public virtual Resturant Resturant { get; set; }
        //public virtual ServiceCategory ServiceCategory { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
