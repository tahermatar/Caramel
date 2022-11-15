using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class ServiceCategory
    {
        public ServiceCategory()
        {
            Meals = new HashSet<Meal>();
            Resturants = new HashSet<Resturant>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ExtraInformation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int Archived { get; set; }

        public virtual ICollection<Meal> Meals { get; set; }
        public virtual ICollection<Resturant> Resturants { get; set; }
    }
}
