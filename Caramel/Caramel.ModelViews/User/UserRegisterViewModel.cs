﻿namespace Caramel.ModelViews.User
{ 
    public class UserRegisterViewModel
    {
        //public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsSuperAdmin { get; set; }

    }
}
