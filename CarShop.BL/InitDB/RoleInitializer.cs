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

        public static async Task InitializerAsync(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            string email = "admin@admin.com";
            string password = "1834asd";

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            if(await userManager.FindByEmailAsync(email) == null)
            {
                User user = new User { Email = email, UserName = email, Basket = new Basket() };
                IdentityResult identityResult = await userManager.CreateAsync(user, password);
                if (identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }


    }
}
