using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews
{
    public enum ServiceCategoryEnum
    {

        [Description("All")]
        All = -1,

        [Description("Online Delhivery")]
        OnlineDelhivery = 0,

        [Description("Pre-booking")]
        Prebooking = 1,

        [Description("Catering")]
        Catering = 2,

        [Description("Table Reservation")]
        TableReservation = 3
    }
}
