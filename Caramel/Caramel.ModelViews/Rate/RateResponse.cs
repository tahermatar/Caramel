using Caramel.Common.Extinsions;
using Caramel.ModelViews.Resturant;
using System.Collections.Generic;

namespace Caramel.ModelViews.Rate
{
    public class RateResponse
    {
        public PagedResult<RateResult> Rate { get; set; }
        public Dictionary<int, ResturantViewAllModelView> Resturant { get; set; }
    }
}
