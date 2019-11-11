using CarShop.BL.Models;
using CarShop.BL.ViewModel;
using CarShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class CarController : Controller
    {
        ContextDb context;
        IWebHostEnvironment webHostEnvironment;
        public readonly UserManager<User> userManager;
        public List<User> Users { get; set; }
        public Category Category { get; set; }
        public User CurrentUser { get; set; }
        public CarController(ContextDb _context, IWebHostEnvironment _webHostEnvironment, UserManager<User> _userManager)
        {
            context = _context;
            webHostEnvironment = _webHostEnvironment;
            userManager = _userManager;
          
            
        }

        
        public IActionResult Index(string category = null)
        {
            IQueryable<Car> cars = context.Cars.Include(x => x.Category);
            if (category !=null && !category.Equals("All"))
            {
                cars = cars.Where(x => x.Category.Name == category);
            }
            Category = context.Categories.FirstOrDefault(x=>x.Name == category);
            
            CarsViewModel model = new CarsViewModel()
            {
                Cars = cars,
                Categories = new SelectList(context.Categories.ToList(), "Name", "", Category),

            };

            return View(model);
        }

        [Route("create-category")]
        public IActionResult CreateCategory()
        {
            var categoriers = context.Categories.ToList();
            if (categoriers.Count() <= 0)
            {
                TempData["Messages"] = true;
            }
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
                return  RedirectToAction("Index", "AdminPanel");
            }
            catch (ArgumentNullException)
            {

                return View();
            }
        }
        [HttpGet]
        public IActionResult AllLogos()
        {

            List<CarLogo> carLogos = context.CarLogos.ToList();
            return View(carLogos);
        }
        
        public IActionResult DeleteLogo(int? id, string name)
        {
            if (id != null && name !=null) {
                CarLogo carLogo = context.CarLogos.FirstOrDefault(x => x.Id == id && x.NameLogo == name);
                if (carLogo != null)
                {
                   
                    context.CarLogos.Remove(carLogo);
                    
                    context.SaveChanges();
                    return RedirectToAction("AllLogos");
                }
            }
            return NotFound();
               
        }
        [HttpGet]
        public IActionResult CreateLogo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateLogo(CarLogo carLogo, IFormFile logo)
        {
            CarLogo carLogo1 = context.CarLogos.FirstOrDefault(x => x.NameLogo == carLogo.NameLogo);
            if (ModelState.IsValid)
            {
                if (carLogo1 == null)
                {
                    if (logo != null)
                    {
                        string path = $"/Img/Cars/Logo/{logo.FileName}";
                        using (FileStream file = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await logo.CopyToAsync(file);
                        }
                        carLogo.Path = path;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Картинка обов'язкова");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Даний логотип вже існує");

                }

                context.CarLogos.Add(carLogo);
                context.SaveChanges();
                return RedirectToAction("Index", "AdminPanel");

            }
            return View();
        }

        public async Task<IActionResult> DetailsCar(string name, int id)
        {
            //User user = null;
            EditCarViewModel model = new EditCarViewModel();
            model.Car = context.Cars.FirstOrDefault(x => x.Id == id && x.Name == name);
            model.Engine = context.Engines.FirstOrDefault(x => x.Id == model.Car.EngineId);
            model.Descriptions = context.Descriptions.Where(x => x.CarId == id).ToList();
            if (User.Identity.Name !=null)
            {
                CurrentUser = await userManager.FindByNameAsync(User.Identity.Name);
                model.Basket = context.Baskets.Include(x=>x.BasketCars).FirstOrDefault(x => x.UserId == CurrentUser.Id);
                model.BasketCar = context.BasketCars.Include(x => x.Car).Where(x => x.BasketId == model.Basket.Id && x.InBasket == true).FirstOrDefault(x => x.CarId == id);

            }
            if (model.Car !=null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "HomeController");
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
        public IActionResult CreateDescription()
        {

            return PartialView("_CreateDescription");
        }

        [HttpPost]
        public IActionResult CreateDescription(Car car, Description description, IFormFile img)
        {
            Car car1 = context.Cars.FirstOrDefault(x => x.Id == description.CarId);

            context.Descriptions.Add(description);
            context.SaveChanges();

            context.Cars.Update(car1);
            context.SaveChanges();
            return RedirectToAction("Index");
            

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
        public IActionResult EditCar(int? id)
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
                        Category = new SelectList(categories, "Name", "Name", car.Category),
                        Categories = categories,
                        CarLogos = context.CarLogos.ToList()
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
                c.CarLogoId = car.CarLogoId;


               

                //context.Cars.Update(car);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                return View();
            }
        }

        public IActionResult EditLogo(int? id, string name)
        {
            if(id !=null && name != null)
            {
                CarLogo carLogo = context.CarLogos.FirstOrDefault(x => x.Id == id && x.NameLogo == name);
                if (carLogo !=null)
                {
                    return View(carLogo);
                }

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditLogo(CarLogo carLogo, IFormFile logo)
        {
            if (ModelState.IsValid)
            {
                CarLogo carLogoToE = context.CarLogos.Find(carLogo.Id);

                if (logo != null)
                {
                    string path = $"/Img/Cars/Logo/{logo.FileName}";
                    using (FileStream file = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await logo.CopyToAsync(file);
                    }
                    carLogo.Path = path;
                }
                else
                {
                    int id = carLogo.Id;
                    carLogo.Path = carLogoToE.Path;
                    context.Entry(carLogoToE).State = EntityState.Deleted;
                }
                   context.CarLogos.Update(carLogo);
                    context.SaveChanges();
                    return RedirectToAction("AllLogos");
                

            }
            return View();
        }

        public IActionResult CreateCar()
        {
            EditCarViewModel model = new EditCarViewModel()
            {
                Categories = context.Categories.ToList(),
                CarLogos = context.CarLogos.ToList()
            };
            if (model.Categories.Count() <= 0)
            {
              
                return RedirectToAction("CreateCategory");
            }
            else
            {
                return View(model);
            }
         }
        //TODO: редагування машини
        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car,Engine engine, IFormFile formFile, IFormFile formFile1)
        {
            try
            {
                string path = $"/Img/Cars/{formFile.FileName}";
 
                using (FileStream file = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await formFile.CopyToAsync(file);
                    
                }
                if (formFile1 != null)
                {
                    string path1 = $"/Img/Cars/{formFile1.FileName}";
                    using (FileStream file1 = new FileStream(webHostEnvironment.WebRootPath + path1, FileMode.Create))
                    {
                        await formFile1.CopyToAsync(file1);
                    }
                    car.BigImagePath = path1;
                }
                else
                {
                    car.BigImagePath = "/Img/Cars/backgroundDetails.jpg";
                }
                car.ImagePath = path;
                context.Add(engine);
                context.SaveChanges();
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
        [HttpPost]
        public  async Task<IActionResult> ChangeBackgroundImage(Car car,IFormFile background)
        {
            Car car1 = context.Cars.Find(car.Id);

            string path = $"/Img/Cars/{background.FileName}";
            using (FileStream file = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
            {
                await background.CopyToAsync(file);
            }
            car1.BigImagePath = path;
            await context.SaveChangesAsync();
            return RedirectToAction("DetailsCar",car.Name, car.Id);

        }
    }
}
