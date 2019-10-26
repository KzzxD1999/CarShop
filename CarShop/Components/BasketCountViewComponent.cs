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
    public class BasketCountViewComponent : ViewComponent
    {
        public ContextDb db;
        public readonly UserManager<User> userManager;
        public BasketCountViewComponent(ContextDb _db, UserManager<User> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }

      

        public string Invoke()
        {

            
            var s = db.Baskets.Include(x => x.BasketCars).ThenInclude(x => x.Car).Where(x => x.User.UserName == User.Identity.Name);

            string countReturn = ""; 
            foreach (var item in s)
            {

                countReturn = item.BasketCars.Select(x => x.Car).Count().ToString();
                if (countReturn == "0")
                {
                    return "пуста";
                }
                return countReturn;
            }
            return countReturn;
        }
    }
}
