using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Order
    {
        public Order()
        {
            Customers = new HashSet<Customer>();
            Resturants = new HashSet<Resturant>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfExecution { get; set; }
        public int MealId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int Archived { get; set; }
        public int ResturantId { get; set; }
        public int Status { get; set; }

        public virtual Meal Meal { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Resturant> Resturants { get; set; }
    }
}
