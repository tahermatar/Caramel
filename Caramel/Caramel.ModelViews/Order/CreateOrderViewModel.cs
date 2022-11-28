using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Order
{
    public class CreateOrderViewModel
    {
        public int CustomerId { get; set; }
        public int MealId { get; set; }
        public int ResturantId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public int? CreatedBy { get; set; }



    }
}
