using Caramel.Common.Extinsions;

namespace Caramel.ModelViews.Resturant
{
    public class ResturantResponse
    {
        public PagedResult<ResturantModelView> Resturants { get; set; }
    }
}
