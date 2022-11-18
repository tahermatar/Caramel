using Caramel.ModelViews.Resturant;

namespace Caramel.Core.Mangers.ResturantManager
{
    public interface IResturantManager : IManager
    {
        public ResturantLoginResponseModelView SignUp(ResturantRegisterViewModel resturantReg);
        public ResturantLoginResponseModelView Login(ResturantLoginModelView resturantLogin);
        public ResturantModelView UpdateProfile(ResturantModelView currentResturant, ResturantModelView request);
        public void DeleteResturant(ResturantModelView currentResturant, int id);
    }
}
