﻿using CarShop.BL.Models;
using CarShop.BL.ViewModel;
using CarShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class CarController : Controller
    {
        ContextDb context;
        IWebHostEnvironment webHostEnvironment;
        public CarController(ContextDb _context, IWebHostEnvironment _webHostEnvironment)
        {
            context = _context;
            webHostEnvironment = _webHostEnvironment;
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
                return  RedirectToRoute("https://localhost:44335/AdminPanel");
            }
            catch (ArgumentNullException)
            {

                return View();
            }
        }
        public IActionResult DetailsCar(string name, int id)
        {
            EditCarViewModel model = new EditCarViewModel();
            model.Car = context.Cars.FirstOrDefault(x => x.Id == id && x.Name == name);
            model.Engine = context.Engines.FirstOrDefault(x => x.Id == model.Car.EngineId);
            if (model.Car !=null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("CreateCar");
            }

        }
        public IActionResult DeleteCategory(string name, int id)
        {
            var categories = context.Categories.FirstOrDefault(x=>x.Name == name && x.Id == id);
            if (categories !=null)
            {
                context.Categories.Remove(categories);
                context.SaveChanges();
                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                return RedirectToAction("Index", "AdminPanel");   
            }
        }
        public IActionResult DeleteCar(string name, int id)
        {
            var car = context.Cars.FirstOrDefault(x => x.Id == id && x.Name == name);
            if(car != null)
            {
                context.Cars.Remove(car);
                context.SaveChanges();
                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                return RedirectToAction("Index", "AdminPanel");
            }


        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id != null) {
                var category = context.Categories.Find(id);
                return View(category);
            }
            else
            {
                return RedirectToAction("Index", "AdminPanel");
            }

        }
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {

            if (category != null)
            {
                context.Categories.Update(category);
                context.SaveChanges();
                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                return View();
            }

        }
        public async Task<IActionResult> EditCar(int? id)
        {
            if (id != null)
            {
                var car = context.Cars.Find(id);

                if(car != null)
                {
                    var categories = context.Categories.ToList();
                    var engine = context.Engines.FirstOrDefault(x => x.Id == car.EngineId);
                    EditCarViewModel model = new EditCarViewModel()
                    {
                        Car = car,
                        Engine = engine,
                        Category = new SelectList(categories,"Name","Name", car.Category),
                        Categories = categories,
                    };

                    return View(model);
                }
                else{
                    return RedirectToAction("Index", "AdminPanel");
                }
            }
            else
            {
                return RedirectToAction("Index", "AdminPanel");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditCar(Car car, IFormFile formFile, IFormFile formFile1)
        {
            if(car != null)
            {
                Car c = context.Cars.FirstOrDefault(x=>x.Id == car.Id);
                if (formFile != null)
                {
                    string path = $"/Img/Cars/{formFile.FileName}";
        
                    using (FileStream file = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                       await formFile.CopyToAsync(file);
                    }
                    car.ImagePath = path;
                }
                if(formFile1 != null)
                {
                    string path1 = $"/Img/Cars/{formFile1.FileName}";

                    using (FileStream file = new FileStream(webHostEnvironment.WebRootPath + path1, FileMode.Create))
                    {
                        await formFile1.CopyToAsync(file);
                    }
                    car.BigImagePath = path1;
                }
                Category category = context.Categories.First(x => x.Name == car.Category.Name);
                Engine engine = context.Engines.First(x => x.Name == car.Engine.Name);

                c.Name = car.Name;
                c.Text = car.Text;
                c.CategoryId = category.Id;
                c.Height = car.Height;
                c.Engine.Name = engine.Name;
                c.Engine.Horsepower = engine.Horsepower;
                c.Color = car.Color;
                c.MaxSpeed = car.MaxSpeed;
                c.Price = car.Price;
                c.Weight = car.Weight;
                c.Width = car.Width;
                
                //TODO: зробити нориальне збереження
                //context.Entry(car).State = EntityState.Modified;


               

                //context.Cars.Update(car);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                return View();
            }
        }
        public IActionResult CreateCar()
        {
            EditCarViewModel model = new EditCarViewModel()
            {
                Categories = context.Categories.ToList()
            };
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car,Engine engine, IFormFile formFile, IFormFile formFile1)
        {
            try
            {
                string path = $"/Img/Cars/{formFile.FileName}";
                string path1 = $"/Img/Cars/{formFile1.FileName}";
                using (FileStream file = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await formFile.CopyToAsync(file);
                    
                }
                using (FileStream file1 = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await formFile1.CopyToAsync(file1);
                }
                //Engine engine = new Engine(engineName, horsepower);
                
                //car.EngineId = engine.Id;
                //car.Engine = engine;
                car.ImagePath = path;
                car.BigImagePath = path1;
                
                context.Add(car);
                context.Add(engine);
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
