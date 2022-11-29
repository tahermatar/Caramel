using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Address
    {
        public Address()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Road { get; set; }
        public string ExtraInformation { get; set; } 

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
