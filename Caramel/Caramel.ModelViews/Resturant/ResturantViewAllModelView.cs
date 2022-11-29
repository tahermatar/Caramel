using Caramel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Resturant
{
    public class ResturantViewAllModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public int? ServiceCategoryId { get; set; }
        public string WorkingTime { get; set; }
        public int IsChef { get; set; }
        public string Phone { get; set; }
        public double TotalRate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Road { get; set; }
        public string ExtraAddressInformation { get; set; }

    }
}
