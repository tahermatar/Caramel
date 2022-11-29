using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Customer
{
    public class AddressResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Road { get; set; }
        public string ExtraInformation { get; set; }
    }
}
