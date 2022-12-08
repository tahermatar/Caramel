using Caramel.ModelViews.Customer;
using Caramel.ModelViews.User;

namespace Caramel.Core.Mangers.CustomerManger
{
    public interface ICustomerManager : IManager
    {
        public CustomerLoginResponseViewModel Rigester(UserModelViewModel currentUser, CustomerRegisterViewModel vm);
        CustomerResult Confirmation(UserModelViewModel currentUser, string ConfirmationLink);
        public CustomerLoginResponseViewModel Login(CustomerLoginViewModel vm);
        public CustomerResult ViewProfile(UserModelViewModel currentUserm);
        public CustomerResult GetCustomer(int id);
        public CustomerResponse GetAllCustomer(UserModelViewModel currentUser,
                                               int page = 1,
                                               int pageSize = 10,
                                               string sortColumn = "",
                                               string sortDirection = "ascending",
                                               string searchText = "");
        public CustomerUpdateModelView UpdateProfile(UserModelViewModel currentUser,
                                                     CustomerUpdateModelView request);
        public AddressResult PutAddress(UserModelViewModel currentUser, AddressResult request);
        public void DeleteCustomer(UserModelViewModel currentUser, int id);
        









    }
}
