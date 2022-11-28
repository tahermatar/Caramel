﻿using Caramel.ModelViews.Customer;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;

namespace Caramel.Core.Mangers.CustomerManger
{
    public interface ICustomerManager : IManger
    {
        public CustomerLoginResponseViewModel Rigester(CustomerRegisterViewModel vm);
        public CustomerResponse GetAll(UserModelViewModel currentUser, int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "");
        CustomerResult Confirmation(string ConfirmationLink);
        public CustomerLoginResponseViewModel Login(CustomerLoginViewModel vm);
        public CustomerUpdateModelView UpdateProfile(CustomerModelViewModel currentUser, CustomerUpdateModelView request);
        public void DeleteCustomer(UserModelViewModel currentUser, int id);
        public CustomerResult GetCustomer(int id);
        public CustomerResult ViewProfile(CustomerModelViewModel currentUser);
        public AddressResult PutAddress(CustomerModelViewModel currentUser, AddressResult request);









    }
}
