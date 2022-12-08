using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Rate
{
    public class AddRateModelView
    {
        public int Id { get; set; }
        public int RateNumber { get; set; }
        public string Review { get; set; }
        //public int? CustomerId { get; set; }
        public int? ResturantId { get; set; }
    }
}
