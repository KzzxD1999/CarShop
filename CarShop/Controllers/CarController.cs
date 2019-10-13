using CarShop.BL.Models;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class CarController : Controller
    {
        ContextDb context;

        public CarController(ContextDb _context)
        {
            context = _context;
        }


        [Route("create-category")]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [Route("create-category")]
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToRoute("https://localhost:44335/AdminPanel");
            }
            catch (ArgumentNullException)
            {

                return View();
            }
        }
        public IActionResult CreateCar()
        {
            var categoies = context.Categories.ToList();
            return View(categoies);
        }
        [HttpPost]
        public IActionResult CreateCar(Car car, string engineName, int horsepower)
        {
            try
            {
                Engine engine = new Engine(engineName, horsepower);
                car.Engine = engine;
                context.Add(car);
                context.SaveChanges();
                return RedirectToRoute("https://localhost:44335/AdminPanel");
            }
            catch (ArgumentNullException)
            {
                return View();
            }
        } 
    }
}
