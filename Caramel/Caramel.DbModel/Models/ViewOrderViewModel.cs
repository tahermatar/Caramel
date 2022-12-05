using System;

namespace Caramel.Models
{
    public class ViewOrderViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MealName { get; set; }
        public int RestorantId { get; set; }
        public string ResturantName { get; set; }
        public float TotalPrice { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfExcution { get; set; }
        public string Status { get; set; }
    }
}
