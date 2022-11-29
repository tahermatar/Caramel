using Caramel.Common.Extinsions;
using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Blog
{
    public class BlogResponse
    {
        public PagedResult<BlogViewModel> Blog { get; set; }
        public Dictionary<int, UserResult> User { get; set; }
    }
}
