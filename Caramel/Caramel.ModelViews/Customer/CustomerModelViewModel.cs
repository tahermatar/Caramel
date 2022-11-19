using Caramel.Models;
using System.Collections.Generic;

namespace Caramel.ModelViews.Customer
{
    public class CustomerModelViewModel
    {
        public CustomerModelViewModel()
        {
            Permissions = new List<Userpermissionview>(); // if in model put a hash set not list
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsSuperAdmin { get; set; }
        public List<Userpermissionview> Permissions { get; set; }
    }
}
