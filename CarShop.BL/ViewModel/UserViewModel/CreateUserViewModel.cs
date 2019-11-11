using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.BL.ViewModel.UserViewModel
{
    public class CreateUserViewModel
    {

        [Required(ErrorMessage ="Ім'я обов'язкове")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Прізвище обов'язкове")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Пошта обов'язкова")]
        [EmailAddress]

        public string Email { get; set; }

        [Required(ErrorMessage ="Вік обов'язковий")]
        public int Age { get; set; }

        [Required(ErrorMessage ="Пароль обо'язковий")]
        public string Password { get; set; }
        public string Avatar { get; set; }
    }
}
