using System;

namespace Caramel.ModelViews.Customer
{ 
    public class CustomerRegisterViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Image { get; set; }
        public string ImageString { get; set; }

    }
}
