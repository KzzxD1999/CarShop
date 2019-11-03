using CarShop.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.BL.ViewModel.UserViewModel
{
    public class UserPageViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }

    }
}
