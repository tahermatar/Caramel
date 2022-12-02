using Caramel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Meal
{
    public class MealCreateModelView
    {

        public int Id { get; set; }
        public string MealName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Component { get; set; }
        public int IsAvailable { get; set; }
        public string ImageString{ get; set; }
        public string Image { get; set; }
        public int ServiceCategoryId { get; set; }
        public int? ResturantId { get; set; }
        public MealCategoryEnum MealCat { get; set; }

    }
}
