using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Order
{
    public class ShowOrdersViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MealName { get; set; }
        public int RestorantId { get; set; }
        public string ResturantName { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfExcution { get; set; }
        public int Status { get; set; }
    }
}
