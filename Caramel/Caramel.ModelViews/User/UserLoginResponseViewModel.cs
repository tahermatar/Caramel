using System.ComponentModel;

namespace Caramel.ModelViews.User
{
    public class UserLoginResponseViewModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        [DefaultValue("")]
        public string Token { get; set; }
    }
}
