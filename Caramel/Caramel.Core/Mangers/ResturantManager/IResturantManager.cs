using Caramel.ModelViews.Order;
using Caramel.ModelViews.Resturant;
using System.Collections.Generic;

namespace Caramel.Core.Mangers.ResturantManager
{
    public interface IResturantManager : IManager
    {
        public ResturantLoginResponseModelView SignUp(ResturantRegisterViewModel resturantReg);
        public ResturantLoginResponseModelView Login(ResturantLoginModelView resturantLogin);
        public List<OrderResult> GetAll();
        public ResturantModelView UpdateProfile(ResturantModelView currentResturant, ResturantModelView request);
        public void DeleteResturant(ResturantModelView currentResturant, int id);
        ResturantModelView Confirmation(string ConfirmationLink);
    }
}
