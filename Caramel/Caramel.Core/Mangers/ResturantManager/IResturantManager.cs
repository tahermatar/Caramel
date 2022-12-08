using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;

namespace Caramel.Core.Mangers.ResturantManager
{
    public interface IResturantManager : IManager
    {
        public ResturantLoginResponseModelView SignUp(UserModelViewModel currentUser,
                                                      ResturantRegisterViewModel resturantReg);
        ResturantRegViewModel Confirmation(UserModelViewModel currentUser, string ConfirmationLink);
        public ResturantLoginResponseModelView Login(ResturantLoginModelView resturantLogin);
        public ResturantViewAllModelView ViewProfile(UserModelViewModel currentUser);
        public ResturantViewAllModelView GetResturant(int id);
        public ResturantResponse GetAll(UserModelViewModel currentUser,
                                        int page = 1,
                                        int pageSize = 10,
                                        string sortColumn = "",
                                        string sortDirection = "ascending",
                                        string searchText = "");
        public ResturantUpdateModelView UpdateProfile(UserModelViewModel currentResturant,
                                                      ResturantModelView request);
        public ResturantModelView UpdateResturantAddress(UserModelViewModel currentUser,
                                                         AddressResult reg);
        public ResturantRegViewModel UpdateRegistrationData(UserModelViewModel currentUser,
                                                            ResturantUpdateRegModelView reg);
        public void DeleteResturant(UserModelViewModel currentResturant, int id);
        
        


        


    }
}
