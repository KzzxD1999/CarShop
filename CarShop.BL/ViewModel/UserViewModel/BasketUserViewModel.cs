using CarShop.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.BL.ViewModel.UserViewModel
{
    public class BasketUserViewModel
    {
        public User User { get; set; }
        public Basket Basket { get; set; }
        public List<BasketCar> Baskets { get; set; }
    }
}
