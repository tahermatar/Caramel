using Caramel.ModelViews.Rate;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.RateManager.cs
{
    public interface IRateManager : IManager
    {
        public AddRateModelView PutRate(UserModelViewModel currentUser,AddRateModelView addRate);
        public RateResponse ViewResturantRate(UserModelViewModel currentUser, 
                                              int page = 1,
                                              int pageSize = 5,
                                              string sortColumn = "",
                                              string sortDirection = "ascending",
                                              string searchText = "");

        public RateResponse ViewResturantRateForUser(UserModelViewModel currentUser,
                                              int page = 1,
                                              int pageSize = 5,
                                              string sortColumn = "",
                                              string sortDirection = "ascending",
                                              string searchText = "");

        public RateResponse ViewCustomerRate(UserModelViewModel currentUser,
                                              int page = 1,
                                              int pageSize = 5,
                                              string sortColumn = "",
                                              string sortDirection = "ascending",
                                              string searchText = "");
        public void DeleteRate(UserModelViewModel currentUser, int id);
    }
}
