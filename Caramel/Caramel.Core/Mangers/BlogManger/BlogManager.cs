using AutoMapper;
using Caramel.Common.Exceptions;
using Caramel.Common.Extinsions;
using Caramel.Data;
using Caramel.DbModel.Models;
using Caramel.ModelViews;
using Caramel.ModelViews.Blog;
using Caramel.ModelViews.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Core.Mangers.BlogManger
{
    public class BlogManager : IBlogManager
    {
        private CaramelDbContext _context;
        private IMapper _mapper;

        public BlogManager(CaramelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void ArchiveBlog(UserModelViewModel currentUser, int id)
        {
            if (!currentUser.IsSuperAdmin)
            {
                throw new ServiceValidationException("You don't have permission to archive blog");
            }

            var data = _context.Blogs
                                    .FirstOrDefault(a => a.Id == id)
                                    ?? throw new ServiceValidationException("Invalid blog id received");
            data.Archived = 1;
            _context.SaveChanges();
        }

        public BlogViewModel GetBlog(UserModelViewModel currentUser, int id)
        {
            var allowedPermissions = new List<string> { "blog_view", "blog_all_view" };
            var hasAccess = currentUser.Permissions.Any(x => allowedPermissions.Contains(x.Code));
            
            var isAllView = currentUser.Permissions.Any(x => x.Code.Equals("blog_all_view") );

            var res = _context.Blogs
                                   .Include("User") // Method for include its good for more one include =>.Include("Include1.Include2")
                                   .FirstOrDefault(a => currentUser.IsSuperAdmin 
                                   || (hasAccess && (isAllView || a.CreatedId ==currentUser.Id) && a.Id == id))
                                   ?? throw new ServiceValidationException("Invalid blog id received");



            return _mapper.Map<BlogViewModel>(res);
        }

        public BlogResponse GetBlogs(int page = 1, 
                                     int pageSize = 5,
                                     StatusEnum statusEnum = StatusEnum.All,
                                     string sortColumn = "",
                                     string sortDirection = "ascending",
                                     string searchText = "")
        {
            var queryRes = _context.Blogs
                                        .Where(a => (statusEnum == StatusEnum.All
                                            || a.Status == (int)statusEnum)
                                            && (string.IsNullOrWhiteSpace(searchText)
                                            || (a.Title.Contains(searchText)
                                            || a.Content.Contains(searchText))));

            if (!string.IsNullOrWhiteSpace(sortColumn)
                && sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderBy(sortColumn);
            }
            else if (!string.IsNullOrWhiteSpace(sortColumn)
                && sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderByDescending(sortColumn);
            }

            var res = queryRes.GetPaged(page, pageSize);

            var userIds = res.Data
                             .Select(a => a.CreatedId)
                             .Distinct()
                             .ToList();

            var users = _context.Users
                                     .Where(a => userIds.Contains(a.Id))
                                     .ToDictionary(a => a.Id, x => _mapper.Map<UserResult>(x));

            var data = new BlogResponse
            {
                Blog = _mapper.Map<PagedResult<BlogViewModel>>(res),
                User = users
            };

            data.Blog.Sortable.Add("Title", "Title");
            data.Blog.Sortable.Add("CreatedDate", "Created Date");

            data.Blog.Filterable.Add("status", new FilterableKeyModel
            {
                Title = "status",
                Values = ((StatusEnum[])Enum.GetValues(typeof(StatusEnum)))
                            .Select(c => new FilterableValueModel
                            {
                                Id = (int)c,
                                Title = c.GetDescription().ToString()
                            })
                            .ToList()
            });

            return data;
        }
        // For Update and Create
        public BlogViewModel PutBlog(UserModelViewModel currentUser, BlogRequest blogRequest)
        {
            Blog blog = null;

            if (!currentUser.IsSuperAdmin)
            {
                throw new ServiceValidationException("You don't have permission to add or update blog");
            }

            if (blogRequest.Id > 0)
            {
                blog = _context.Blogs
                                    .FirstOrDefault(a => a.Id == blogRequest.Id)
                                    ?? throw new ServiceValidationException("Invalid blog id received");

                blog.Title = blogRequest.Title;
                blog.Content = blogRequest.Content;
                blog.Status = (int)blogRequest.Status;
            }
            else
            {
                blog = _context.Blogs.Add(new Blog
                {
                    Title = blogRequest.Title,
                    Content = blogRequest.Content,
                    CreatedId = currentUser.Id,
                    Status = (int)blogRequest.Status
                }).Entity;
            }

            _context.SaveChanges();
            return _mapper.Map<BlogViewModel>(blog);
        }
    }
}
