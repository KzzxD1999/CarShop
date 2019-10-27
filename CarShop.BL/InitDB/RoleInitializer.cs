using CarShop.BL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.BL.InitDB
{
    public class RoleInitializer
    {

        public static async Task InitializerAsync(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }


    }
}
