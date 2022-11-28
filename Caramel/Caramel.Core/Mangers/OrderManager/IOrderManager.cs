using Caramel.Models;
using Caramel.ModelViews.Order;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.OrderManager
{
    public interface IOrderManager
    {
        CreateOrderViewModel CreateMealOrder(UserModelViewModel currentUser ,CreateOrderViewModel vm);
        //OrderResponse ViewMealOrder(UserModelViewModel currentUser ,int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "");
        List<ViewOrderViewModel> ViewMealOrder(UserModelViewModel currentUser );
        void FinishMealOrder(UserModelViewModel currentUser, int id);
        void DeleteMealOrder(UserModelViewModel currentUser ,int id);
    }
}
