using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Resturant
{
    public class ServiceCategoryModelView
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ExtraInformation { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
