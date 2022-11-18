using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.UserManger
{
    public interface IUserManger : IManager
    {
        public UserModelViewModel UpdateProfile(UserModelViewModel currentUser, UserModelViewModel request);
        public UserLoginResponseViewModel Login(UserLoginViewModel vm);
        public UserLoginResponseViewModel Rigester(UserRegisterViewModel vm);
        public void DeleteUser(UserModelViewModel currentUser, int id);
    }
}
