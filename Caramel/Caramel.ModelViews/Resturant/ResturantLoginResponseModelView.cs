using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Resturant
{
    public class ResturantLoginResponseModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue("")]
        public string Image { get; set; }

        [DefaultValue("")]
        public string Bio { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
