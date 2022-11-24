using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Rate
    {
        public Rate()
        {
            Customers = new HashSet<Customer>();
            Resturants = new HashSet<Resturant>();
        }

        public int Id { get; set; }
        public int RateNumber { get; set; }
        public string Review { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool Archived { get; set; }
        public int? CustomerId { get; set; }
        public int? ResturantId { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Resturant> Resturants { get; set; }
    }
}
