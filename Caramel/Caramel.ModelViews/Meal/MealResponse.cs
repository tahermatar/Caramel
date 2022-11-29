using Caramel.Common.Extinsions;
using Caramel.ModelViews.Resturant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Meal
{
    public class MealResponse
    {
        public PagedResult<MealResult> Meals { get; set; }

    }
}
