using Caramel.ModelViews.Customer;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.CustomerManger
{
    public interface ICustomerManger : IManger
    {
        public CustomerLoginResponseViewModel Rigester(CustomerRegisterViewModel vm);
        public List<CustomerResult> GetAll();
        CustomerResult Confirmation(string ConfirmationLink);
        public CustomerLoginResponseViewModel Login(CustomerLoginViewModel vm);
        public CustomerUpdateModelView UpdateProfile(CustomerModelViewModel currentUser, CustomerUpdateModelView request);







    }
}
