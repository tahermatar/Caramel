﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Order
{
    public class OrderInfoViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string MealName { get; set; }
        public string ResturantName { get; set; }
        public float TotalPrice { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfExcution { get; set; }
        public string Status { get; set; }
    }
}
