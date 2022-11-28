using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Resturant
{
    public class ImageModelView
    {
        public int Id { get; set; }
        public string Image1 { get; set; }
        public string ImageString { get; set; }
        public string Title { get; set; }
        public string ExtraInformation { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
