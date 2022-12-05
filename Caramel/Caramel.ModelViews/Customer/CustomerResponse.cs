using Caramel.Common.Extinsions;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Customer
{
    public class CustomerResponse
    {
        public PagedResult<CustomerResult> Customer { get; set; }
        //public Dictionary<int, AddressResult> address { get; set; }


    }
}
