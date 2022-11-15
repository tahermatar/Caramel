using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.CommonManger
{
    public interface ICommonManager : IManger
    {
        UserModelViewModel GetUserRole(UserModelViewModel user);

    }
}
