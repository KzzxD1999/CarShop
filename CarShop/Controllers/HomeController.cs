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

       
        public IActionResult Index()
        {
            ViewBag.Cabrilets = context.Cars.Where(x => x.Category.Name == "Кабріолет").Take(5);
            ViewBag.X = context.Cars.Where(x => x.Category.Name == "XXXXX").Take(5);
            CarsViewModel carsViewModel = new CarsViewModel()
            {
                Cars = context.Cars.ToList(),
                
                
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
