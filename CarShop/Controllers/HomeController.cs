using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarShop.Models;
using CarShop.BL.ViewModel;
using CarShop.BL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ContextDb context;

        public Category Category { get; set; }
        public readonly UserManager<User> userManager;
        public HomeController(ILogger<HomeController> logger, ContextDb _context, UserManager<User> _userManager)
        {
            _logger = logger;
            context = _context;
            userManager = _userManager;
        }

       
        public async Task<IActionResult> Index()
        {
            ViewBag.Cabrilets = context.Cars.Where(x => x.Category.Name == "Кабріолет").Include(x=>x.CarLogo).Take(5);
            ViewBag.X = context.Cars.Where(x => x.Category.Name == "Легкова").Include(x => x.CarLogo).Take(5);
            List<BasketCar> basketCars = null;
            if (User.Identity.IsAuthenticated)
            {
                User currentUser = await userManager.FindByNameAsync(User.Identity.Name);
                var basket = context.Baskets.FirstOrDefault(x => x.UserId == currentUser.Id);
                //var cars = context.Cars.Where(x=>x.BasketCars == basket.BasketCars);
                basketCars = context.BasketCars.Include(x => x.Car).Where(x => x.Basket == basket && x.InBasket == true).ToList();

            }
           


            CarsViewModel carsViewModel = new CarsViewModel()
            {
                Cars = context.Cars.Include(x=>x.CarLogo).ToList(),
                Basket = basketCars                               
            };
            return View(carsViewModel);
        }
       
      
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
