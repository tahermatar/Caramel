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

        public ResturantViewAllModelView ViewProfile(UserModelViewModel currentUser, int id);
        ResturantRegViewModel Confirmation(UserModelViewModel currentUser, string ConfirmationLink);
        public ResturantResponse GetAll(UserModelViewModel currentUser,
                                        int page = 1,
                                        int pageSize = 10,
                                        string sortColumn = "",
                                        string sortDirection = "ascending",
                                        string searchText = "");

        public ResturantRegViewModel UpdateRegistrationData(UserModelViewModel currentUser, ResturantRegViewModel reg );
        public ResturantModelView UpdateResturantAddress(UserModelViewModel currentUser, AddressResult reg );
        public ResturantViewAllModelView UpdateProfile(UserModelViewModel currentResturant, ResturantModelView request);

        


    }
}
