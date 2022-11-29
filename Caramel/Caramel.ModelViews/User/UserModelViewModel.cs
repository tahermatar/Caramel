using Caramel.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Caramel.ModelViews.User
{
    public class UserModelViewModel
    {
        public UserModelViewModel()
        {
            Permissions = new List<Userpermissionview>(); // if in model put a hash set not list
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public bool IsSuperAdmin { get; set; }
        public List<Userpermissionview> Permissions { get; set; }
    }
}
