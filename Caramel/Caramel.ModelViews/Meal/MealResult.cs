using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Meal
{
    public class MealResult
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public int MealCategoryId { get; set; }
        public double Price { get; set; }
        public int IsAvailable { get; set; }
        public string Image { get; set; }
        public int? ResturantId { get; set; }
    }
}
