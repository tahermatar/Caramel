using Caramel.Common.Extinsions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Order
{
    public class OrderResponse
    {
        public PagedResult<ShowOrdersViewModel> Orders { get; set; }
    
    }
}
