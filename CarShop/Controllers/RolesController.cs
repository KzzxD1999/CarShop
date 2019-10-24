using CarShop.BL.Models;
using CarShop.BL.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<User> userManager;

        public RolesController(RoleManager<IdentityRole> _roleManager, UserManager<User> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult CreateRole() => View();
        

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            return View(name);
        }

        public IActionResult UserList() => View(userManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            User user = await userManager.FindByIdAsync(userId);
            if (user != null) 
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel()
                {
                    AllRoles = allRoles,
                    UserEmail = user.Email,
                    UserId = user.Id,
                    UserRoles = userRoles
                };
                return View(model);
            }
            return NotFound();
        }
       
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            User user = await userManager.FindByIdAsync(userId);
            if (user !=null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removeRoles = userRoles.Except(roles);

                await userManager.AddToRolesAsync(user, addedRoles);
                await userManager.RemoveFromRolesAsync(user, removeRoles);
                return RedirectToAction("Index");

            }
            return NotFound();
        }
    }
}
