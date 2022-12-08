using System;

namespace Caramel.Models
{
    public partial class Rate
    {
        public Rate()
        {
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

        public virtual Resturant Resturant { get; set; }
    }
}
