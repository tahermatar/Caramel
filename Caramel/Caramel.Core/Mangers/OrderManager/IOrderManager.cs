using Caramel.Models;
using Caramel.ModelViews.Order;
using Caramel.ModelViews.User;
using System.Collections.Generic;

namespace Caramel.Core.Mangers.OrderManager
{
    public interface IOrderManager
    {
        public CreateOrderViewModel CreateMealOrder(UserModelViewModel currentUser, 
                                                    CreateOrderViewModel vm);
        public List<ViewOrderViewModel> ViewMealOrder(UserModelViewModel currentUser );
        public void FinishMealOrder(UserModelViewModel currentUser, int id);
        public void DeleteMealOrder(UserModelViewModel currentUser ,int id);
    }
}
