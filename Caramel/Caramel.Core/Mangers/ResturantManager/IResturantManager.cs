using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;

namespace Caramel.Core.Mangers.ResturantManager
{
    public interface IResturantManager : IManager
    {
        public ResturantLoginResponseModelView SignUp(ResturantRegisterViewModel resturantReg);
        public ResturantLoginResponseModelView Login(ResturantLoginModelView resturantLogin);
        public ResturantModelView UpdateProfile(UserModelViewModel currentResturant, ResturantModelView request);
        public void DeleteResturant(UserModelViewModel currentResturant, int id);

        public ResturantModelView ViewProfile(UserModelViewModel currentUser );

    }
}
