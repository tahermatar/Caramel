using System;

namespace Caramel.ModelViews.Rate
{
    public class RateResult
    {
        public int Id { get; set; }
        public int RateNumber { get; set; }
        public string Review { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CustomerId { get; set; }
        public int? ResturantId { get; set; }
    }
}
