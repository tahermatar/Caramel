using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews
{
    public enum MealCategoryEnum
    {

        [Description("All")]
        All = -1,

        [Description("Cakes")]
        Cakes = 0,

        [Description("Cold Sweets")]
        ColdSweets = 1,

        [Description("Eastern Sweets")]
        EasternSweets = 2,

        [Description("Pies And Pastries")]
        PiesAndPastries = 3,

        [Description("Chocolate And Candy")]
        ChocolateAndCandy = 4,

        [Description("Ice Cream")]
        IceCream = 5,

        [Description("Turkish Sweets")]
        TurkishSweets = 6,

        [Description("Arabic Sweets")]
        ArabicSweets = 7
    }
}
