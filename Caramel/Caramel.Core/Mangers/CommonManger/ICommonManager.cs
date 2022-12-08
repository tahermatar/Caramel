using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;

namespace Caramel.Core.Mangers.CommonManger
{
    public interface ICommonManager : IManager
    {
        UserModelViewModel GetUserRole(UserModelViewModel user);
        //UserModelViewModel GetResturanRole(UserModelViewModel resturan);
        //public UserModelViewModel GetCustomerRole(UserModelViewModel customer);
    }
}
