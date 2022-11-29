using Caramel.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Blog
{
    public interface IBlogManager
    {
        void ArchiveBlog(UserModelViewModel currentUser, int id);
        BlogViewModel GetBlog(UserModelViewModel currentUser, int id);
        BlogResponse GetBlogs(int page = 1,
                             int pageSize = 10,
                             StatusEnum statusEnum = StatusEnum.All,
                             string sortColumn = "",
                             string sortDirection = "ascending",
                             string searchText = "");

        BlogViewModel PutBlog(UserModelViewModel currentUser, BlogRequest blogRequest);

       
    
    }
}
