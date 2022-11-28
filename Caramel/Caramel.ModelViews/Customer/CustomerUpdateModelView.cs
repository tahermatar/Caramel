using Caramel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.ModelViews.Customer
{
    public class CustomerUpdateModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int Archived { get; set; }
        public string Phone { get; set; }


    }
}
