using Caramel.ModelViews.Meal;
using Caramel.ModelViews.User;

namespace Caramel.Core.Mangers.MealManager
{
    public interface IMealManager : IManager
    {
        public MealCreateModelView PutMeal(UserModelViewModel currentUser, MealCreateModelView vm);
        public MealResponse GetResturantAllMeal(UserModelViewModel currentUser,
                                        int ResturantId,
                                        int page = 1,
                                        int pageSize = 10,
                                        string sortColumn = "",
                                        string sortDirection = "ascending",
                                        string searchText = "");

        public MealModelView viewMeal(int id);
        public bool DeleteMeal(UserModelViewModel currentUser, int id);
    }
}
