
using Caramel.Models;
using System;
using System.ComponentModel;

namespace Caramel.ModelViews.Customer
{
    public class CustomerResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public virtual Address Address { get; set; }
    }
}
