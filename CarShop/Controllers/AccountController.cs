using CarShop.BL;
using CarShop.BL.Models;
using CarShop.BL.ViewModel;
using CarShop.Models;
using CarShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private IWebHostEnvironment webHostEnvironment;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager, RoleManager<IdentityRole> _roleManager, IWebHostEnvironment _webHostEnvironment)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            webHostEnvironment = _webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
                string path = "/Img/Avatar/" + avatar.FileName;
                using (FileStream file = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await avatar.CopyToAsync(file);
                }






                User user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Email = model.Email,
                    UserName = model.Email,
                    Avatar = path,
                    Basket = new Basket()
                };

                var role =  roleManager.Roles.FirstOrDefault(x=>x.Name == "User").ToString();
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)

                {
                    await userManager.AddToRoleAsync(user, role);




                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callBack = Url.Action("ConfirmMail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Підтвердіть свій Email",
                        $"Підтвердіть реєстрацію <a href='{callBack}'>Тиц</a> "
                        );

                    await signInManager.SignInAsync(user, false);
                    string isUser = userManager.GetRolesAsync(user).Result.ToString();
                    if (isUser.Contains("User"))
                    {
                        return RedirectToAction("Index", "UserPanel");
                    }                    
                    else
                    {
                        return RedirectToAction("Index", "AdminPanel");
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmMail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }

        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
          
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {

   
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логін і (або) пароль");
                }
            }
            return View(model);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
