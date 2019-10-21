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
    public class UsersController : Controller
    {
        public readonly UserManager<User> userManager;
        public UsersController(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }

        [Route("users")]
        public IActionResult Index()
        {
            var user = userManager.Users.ToList();
            return View(user);
        }
        
        public IActionResult CreateUser()
        {
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    FirstName = createUserViewModel.FirstName,
                    LastName = createUserViewModel.LastName,
                    Age = createUserViewModel.Age,
                    Email = createUserViewModel.Email,
                    UserName = createUserViewModel.Email,

                };
                var result = await userManager.CreateAsync(user, createUserViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminPanel");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            return View(createUserViewModel);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            if (id != null)
            {
                var user =  await userManager.FindByIdAsync(id);
                EditUserViewModel model = new EditUserViewModel
                {
                    Age = user.Age,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName
                };
                return View(model);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if(user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.Age = model.Age;
                    var result = await userManager.UpdateAsync(user);
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
            }
            return View(model);
        }

    }
}
