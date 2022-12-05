using Caramel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Meal
{
    public class MealModelView
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public int MealCategoryId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Component { get; set; }
        public int IsAvailable { get; set; }
        public int Archived { get; set; }
        public int ServiceCategoryId { get; set; }
        public int? ResturantId { get; set; }
        public string Image { get; set; }
        //public virtual MealCategory MealCategory { get; set; }
        //public virtual ServiceCategory ServiceCategory { get; set; }


    }
}
