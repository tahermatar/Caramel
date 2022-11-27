using Caramel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Resturant
{
    public class MealModelView
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public int MealCategoryId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Component { get; set; }
        public bool IsAvailable { get; set; }
        public int ImageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ServiceCategoryId { get; set; }
        //public virtual ResturantModelView Resturant { get; set; }
        //public virtual Image Image { get; set; }
        //public virtual MealCategory MealCategory { get; set; }
        //public virtual ServiceCategoryModelView ServiceCategory { get; set; }
    }
}
