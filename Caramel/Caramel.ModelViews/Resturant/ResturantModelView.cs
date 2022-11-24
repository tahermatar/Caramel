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
        public string Name { get; set; }

        [DefaultValue("")]
        public string Image { get; set; }
        public string ImageString { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
    }
}
