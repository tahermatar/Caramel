using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;

namespace Caramel.Core.Mangers.ResturantManager
{
    public interface IResturantManager : IManager
    {
        public ResturantLoginResponseModelView SignUp(UserModelViewModel currentUser, ResturantRegisterViewModel resturantReg);
        public ResturantLoginResponseModelView Login(ResturantLoginModelView resturantLogin);

        public void DeleteResturant(UserModelViewModel currentResturant, int id);

        public ResturantViewAllModelView ViewProfile(UserModelViewModel currentUser );

        public ResturantRegViewModel UpdateRegistrationData(UserModelViewModel currentUser, ResturantRegViewModel reg );
        public ResturantModelView UpdateResturantAddress(UserModelViewModel currentUser, AddressResult reg );
        public ResturantViewAllModelView UpdateProfile(UserModelViewModel currentResturant, ResturantModelView request);


    }
}
