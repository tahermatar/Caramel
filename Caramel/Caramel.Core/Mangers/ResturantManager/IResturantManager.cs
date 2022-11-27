using Caramel.ModelViews.Order;
using Caramel.ModelViews.Resturant;
using System.Collections.Generic;

namespace Caramel.Core.Mangers.ResturantManager
{
    public interface IResturantManager : IManager
    {
        public ResturantLoginResponseModelView SignUp(ResturantRegisterViewModel resturantReg);
        public ResturantLoginResponseModelView Login(ResturantLoginModelView resturantLogin);
        public ResturantModelView UpdateProfile(ResturantModelView currentResturant, ResturantModelView request);
        public void DeleteResturant(ResturantModelView currentResturant, int id);
        public ResturantModelView Confirmation(string ConfirmationLink);
        public MealModelView PutMeal(ResturantModelView currentResturant, MealRequest mealRequest);
        public MealCategoryModelView PutMealCategory(ResturantModelView currentResturant, CategoryRequest categoryRequest);
        public ImageModelView PutImage(ResturantModelView currentResturant, ImageRequest imageRequest);
        public ServiceCategoryModelView PutServiceCategory(ResturantModelView currentResturant, ServiceCategoryRequest serviceCategoryRequest);
    }
}
