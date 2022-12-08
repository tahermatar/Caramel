using Caramel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Resturant
{
    public class ResturantModelView
    {
        public int Id { get; set; }
        public string Bio { get; set; }   
        public int? ServiceCategoryId { get; set; }
        public string WorkingTime { get; set; }
        public bool IsChef { get; set; }
        public string Phone { get; set; }

        [DefaultValue("")]
        public string Image { get; set; }
        public string ImageString { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Road { get; set; }
        public string ExtraAddressInformation { get; set; }
    }
}
