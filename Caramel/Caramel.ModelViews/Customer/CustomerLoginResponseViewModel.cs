using System.ComponentModel;

namespace Caramel.ModelViews.Customer
{
    public class CustomerLoginResponseViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [DefaultValue("")]
        public string Token { get; set; }
    }
}
