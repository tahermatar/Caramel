using Caramel.Common.Extinsions;
using Caramel.Core.Mangers.CommonManger;
using Caramel.ModelViews.Customer;
using Caramel.ModelViews.Resturant;
using Caramel.ModelViews.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace Caramel.Controllers
{

    public class ApiBaseController : Controller
    {
        private UserModelViewModel _loggedInUser;
        public ApiBaseController()
        {
        }
        public UserModelViewModel LoggedInUser
        {
            get
            {
                if (_loggedInUser != null)
                {
                    return _loggedInUser;
                }

                Request.Headers.TryGetValue("Authorization", out StringValues Token);

                if (string.IsNullOrWhiteSpace(Token))
                {
                    _loggedInUser = null;
                    return _loggedInUser;
                }

                var ClaimId = User.Claims.FirstOrDefault(c => c.Type == "Id");

                _ = int.TryParse(ClaimId.Value, out int idd);

                if (ClaimId == null || !int.TryParse(ClaimId.Value, out int id))
                {
                    throw new ServiceValidationException(401, "Invalid or expired token");
                }

                var commonManager = HttpContext.RequestServices.GetService(typeof(ICommonManager)) as ICommonManager;
                //if (id > 1000 && id < 10000)
                //{
                //    _loggedInUser = commonManager.GetCustomerRole(new UserModelViewModel { Id = id });

                //}else if (id > 10000)
                //{
                //    _loggedInUser = commonManager.GetResturanRole(new UserModelViewModel { Id = id });

                //}
                //else 
                _loggedInUser = commonManager.GetUserRole(new UserModelViewModel { Id = id });

                return _loggedInUser;
            }
        }

    }
}
