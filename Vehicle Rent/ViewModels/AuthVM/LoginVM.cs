﻿namespace Vehicle_Rent.ViewModels.AuthVM
{
    public class LoginVM
    {
        public string Email { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}