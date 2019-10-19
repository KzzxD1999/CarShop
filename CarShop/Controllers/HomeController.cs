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

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ContextDb context;

        public Category Category { get; set; }

        public HomeController(ILogger<HomeController> logger, ContextDb _context)
        {
            _logger = logger;
            context = _context;
        }

        public IActionResult Index(string category)
        {
            IQueryable<Car> cars = context.Cars.Include(x => x.Category);
            if (category != null && !category.Equals("All")) {
                cars = cars.Where(x => x.Category.Name == category);
            }

            //ViewBag.categories = context.Categories.ToList();
            Category = context.Categories.FirstOrDefault(x => x.Name == category);
            //if (Category ==null)
            //{
            //    Category = new Category();
            //    Category.Name = "All";
            //}
            //categories.Insert(0, new Category { Name = "Всі", Id = 0 });
            CarsViewModel carsViewModel = new CarsViewModel()
            {
                Cars = cars.ToList(),
                Categories = new SelectList(context.Categories, "Name","", Category),
               
                
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
