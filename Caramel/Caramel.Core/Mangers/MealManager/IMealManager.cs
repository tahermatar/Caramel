using Caramel.ModelViews;
using Caramel.ModelViews.Meal;
using Caramel.ModelViews.User;

namespace Caramel.Core.Mangers.MealManager
{
    public interface IMealManager : IManager
    {
        public MealCreateModelView PutMeal(UserModelViewModel currentUser, MealCreateModelView vm);
        public MealResponse GetResturantAllMeal(UserModelViewModel currentUser,
                                                int ResturantId,
                                                MealCategoryEnum MealCat = MealCategoryEnum.All,
                                                ServiceCategoryEnum ServiceCat = ServiceCategoryEnum.All,
                                                int page = 1,
                                                int pageSize = 10,
                                                string sortColumn = "",
                                                string sortDirection = "ascending",
                                                string searchText = "");

        public MealModelView ViewMeal(int id);
        public void DeleteMeal(UserModelViewModel currentUser, int id);
    }
}
