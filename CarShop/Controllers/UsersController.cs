﻿using CarShop.BL.Models;
using CarShop.BL.ViewModel;
using CarShop.BL.ViewModel.UserViewModel;
using CarShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        public readonly UserManager<User> userManager;
        private ContextDb contextDb;
        private IWebHostEnvironment webHostEnvironment;

        public UsersController(UserManager<User> _userManager, ContextDb _contextDb, IWebHostEnvironment _webHostEnvironment)
        {
            userManager = _userManager;
            contextDb = _contextDb;
            webHostEnvironment = _webHostEnvironment;
        }

        [Route("users")]
        public async Task<IActionResult> Index(string name = null, int page = 1)
        {
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            int pageCount = 15;
            //TODO:Пофіксити пошук
            IQueryable<User> user = null;
            List<User> items = null;
            int count = 0;
            if (name == null)
            {
                
                user = userManager.Users.Where(x => x.Id != currentUser.Id);
                count = await user.CountAsync();
                items = await user.Skip((page - 1) * pageCount).Take(pageCount).ToListAsync();

            }
            else
            {
                items = await userManager.Users.Where(x => x.FirstName == name || x.Email ==name || x.LastName == name && x.Id == currentUser.Id).ToListAsync();
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageCount);
            UserPageViewModel model = new UserPageViewModel()
            {
                Users = items,
                PageViewModel = pageViewModel,
            };

            return View(model);

        }



        public IActionResult DetailsUser(string id, string userName)
        {
            if (id !=null &&  userName !=null)
            {
                
                var user = userManager.Users.FirstOrDefault(x => x.Id == id && x.UserName == userName);
                var basket = contextDb.Baskets.FirstOrDefault(x => x.UserId == id);
                var basketCars = contextDb.BasketCars.Include(x=>x.Car).Where(x=>x.BasketId == basket.Id).ToList();
                BasketUserViewModel model = new BasketUserViewModel()
                {
                    User = user,
                    Basket = basket,
                    Baskets = basketCars,
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult CreateUser()
        {
            return View();
        } 


        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel createUserViewModel, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {

                string path = "";
                if(avatar != null)
                {
                    path = $"/Img/Avatar/{avatar.FileName}";
                    using (FileStream file = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await avatar.CopyToAsync(file);
                    }
                }
                else
                {
                    path = $"/Img/Avatar/avatar.jpg";
                }


                User user = new User()
                {
                    FirstName = createUserViewModel.FirstName,
                    LastName = createUserViewModel.LastName,
                    Age = createUserViewModel.Age,
                    Email = createUserViewModel.Email,
                    UserName = createUserViewModel.Email,
                    Avatar = path,
                    Basket = new Basket()

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
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id != null)
            {
                User user = await userManager.FindByIdAsync(id);
                var basket = contextDb.Baskets.FirstOrDefault(x=>x.UserId == user.Id);
                if(basket != null)
                {
                    contextDb.Baskets.Remove(basket);
                
                }
                await userManager.DeleteAsync(user);
                
                contextDb.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}
