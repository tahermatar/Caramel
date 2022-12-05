using Caramel.ModelViews.User;

namespace Caramel.Core.Mangers.UserManger
{
    public interface IUserManager : IManager
    {
        public UserModelViewModel UpdateProfile(UserModelViewModel currentUser, UserModelViewModel request);
        public UserLoginResponseViewModel Login(UserLoginViewModel vm);
        public UserLoginResponseViewModel SignUp(UserRegisterViewModel vm);
        public void DeleteUser(UserModelViewModel currentUser, int id);
    }
}
