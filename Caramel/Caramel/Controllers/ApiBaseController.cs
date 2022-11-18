
using Caramel.Common.Exceptions;
using Caramel.Common.Extinsions;
using Caramel.Core.Mangers.CommonManger;
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
        private ResturantModelView _loggedInResturant;
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

                int.TryParse(ClaimId.Value, out int idd);

                if (ClaimId == null || !int.TryParse(ClaimId.Value, out int id))
                {
                    throw new ServiceValidationException(401, "Invalid or expired token");
                }

                var commonManager = HttpContext.RequestServices.GetService(typeof(ICommonManager)) as ICommonManager;

                _loggedInUser = commonManager.GetUserRole(new UserModelViewModel { Id = id });

                return _loggedInUser;
            }
        }

        public ResturantModelView LoggedInResturant
        {
            get
            {
                if (_loggedInResturant != null)
                {
                    return _loggedInResturant;
                }

                Request.Headers.TryGetValue("Authorization", out StringValues Token);

                if (string.IsNullOrWhiteSpace(Token))
                {
                    _loggedInResturant = null;
                    return _loggedInResturant;
                }

                var ClaimId = User.Claims.FirstOrDefault(c => c.Type == "Id");

                int.TryParse(ClaimId.Value, out int idd);

                if (ClaimId == null || !int.TryParse(ClaimId.Value, out int id))
                {
                    throw new ServiceValidationException(401, "Invalid or expired token");
                }

                var commonManager = HttpContext.RequestServices.GetService(typeof(ICommonManager)) as ICommonManager;

                _loggedInResturant = commonManager.GetResturanRole(new ResturantModelView { Id = id });

                return _loggedInResturant;
            }
        }
    }
}
