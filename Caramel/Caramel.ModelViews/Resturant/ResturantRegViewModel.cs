using System;

namespace Caramel.ModelViews.Resturant
{
    public class ResturantRegViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
