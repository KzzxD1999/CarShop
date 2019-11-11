using CarShop.BL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Components
{
    public class UserAvatarViewComponent : ViewComponent
    {
        public User CurrentUser { get; set; }
        public readonly UserManager<User> userManager;

        public UserAvatarViewComponent(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }
 
        public string Invoke()
        {
            CurrentUser = userManager.FindByNameAsync(User.Identity.Name).Result;

            if (CurrentUser.Avatar != null)
            {
                return CurrentUser.Avatar;
            }
            else
            {
                return "/Img/Avatar/avatar.jpg";
            }

        }

    }
}
