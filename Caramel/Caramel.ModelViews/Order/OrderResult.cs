using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Order
{
    public class OrderResult
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfExecution { get; set; }
        public int MealId { get; set; }
        public int ResturantId { get; set; }
        public int Status { get; set; }
    }
}
