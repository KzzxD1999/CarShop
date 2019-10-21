using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.BL.ViewModel.UserViewModel
{
    public class CreateUserViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int Age { get; set; }
        public string Password { get; set; }
        
    }
}
