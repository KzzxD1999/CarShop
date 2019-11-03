using CarShop.BL.Models;
using CarShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Components
{
    public class NotificationLoginInViewComponent : ViewComponent
    {
        public ContextDb db;
        public readonly UserManager<User> userManager;
        public User CurrentUser { get; set; }
        public NotificationLoginInViewComponent(ContextDb _db, UserManager<User> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
        public string Invoke()
        {
            CurrentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
                return $"Привіт {CurrentUser.FirstName} {CurrentUser.LastName} !";
          
        }
    }
}
