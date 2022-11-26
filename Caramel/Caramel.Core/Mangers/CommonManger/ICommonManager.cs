using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.CommonManger
{
    public interface ICommonManager : IManager
    {
        UserModelViewModel GetUserRole(UserModelViewModel user);
        ResturantModelView GetResturanRole(ResturantModelView resturan);
        CustomerModelViewModel GetCustomerRole(CustomerModelViewModel customer);
        public UserModelViewModel GetCustomerRole(UserModelViewModel customer);
    }
}
