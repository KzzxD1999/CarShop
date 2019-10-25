using CarShop.BL.Models;
using CarShop.BL.ViewModel;
using CarShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class BasketController : Controller
    {
        private ContextDb contextDb;
        private readonly UserManager<User> userManager;
        private Basket Basket { get; set; }
        private User CurrentUser { get; set; }
        public BasketController(ContextDb _contextDb, UserManager<User> _userManager)
        {
            contextDb = _contextDb;
            userManager = _userManager;
        }

        public IActionResult Index()
        {

            var currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;

            var baskets = contextDb.Baskets.Include(x => x.BasketCars).ThenInclude(x => x.Car).Where(x=>x.UserId == currentUser.Id);
            var basket = contextDb.Baskets.FirstOrDefault(x => x.UserId == currentUser.Id);
            
           

            List <Car> cars = null;


            var basketCars = contextDb.BasketCars.Where(x=>x.BasketId == basket.Id).ToList();
            foreach (var item in baskets)
            {
                cars = item.BasketCars.Select(x => x.Car).ToList(); 
   
  
            }
            UserBasketViewModel model1 = new UserBasketViewModel()
            {
                Baskets = baskets,
                Cars = cars,
                BasketCar = basketCars

            };
            
            return View(model1); //model1


        }



        public IActionResult DeleteCarFromBasket(int? id)
        {
            var user = contextDb.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var basket = contextDb.Baskets.FirstOrDefault(x => x.UserId == user.Id);
            var car = contextDb.BasketCars.FirstOrDefault(x => x.CarId == id && x.BasketId == basket.Id);
            
            contextDb.BasketCars.Remove(car);
            contextDb.SaveChanges();
            return RedirectToAction("Index");



        }


        

        public IActionResult AddCar(int? id, BasketCar basketCar)
        {
            var currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
            if (id != null && ModelState.IsValid)
            {
                var car = contextDb.Cars.FirstOrDefault(x => x.Id == id);

                if (car != null)
                {


                    //var mode = contextDb.Users.Include(x => x.Basket)..FirstOrDefault(x => x.Id == currentUser.Id);
                    var mode = contextDb.Baskets.Include(x => x.BasketCars).Where(x => x.UserId == currentUser.Id);
                    if (mode != null)
                    {
                        //var us = contextDb.Users.Include(x=>x.Basket).ThenInclude(x=>x.Cars).FirstOrDefault(x=>x.UserName == currentUser.UserName);
                        //us.Basket.Cars.Add(car);


                        // mode.Basket.Cars.Add(car);


                        //Basket.Cars.Add(car);

                        Basket = contextDb.Baskets.Where(x => x.UserId == currentUser.Id).FirstOrDefault(x=>x.UserId == currentUser.Id);

                        Basket.BasketCars.Add(new BasketCar { BasketId = Basket.Id, CarId = car.Id, InBasket = true, Count = basketCar.Count });
                        //TODO: змінювати InBasket на тру
                       
                       

                        contextDb.SaveChanges();
                    }
                    else
                    {
                        Basket = new Basket()
                        {
                            //Cars = new List<Car>(),
                            NameBasket = currentUser.UserName,
                            User = currentUser,
                            UserId = currentUser.Id,

                        };
                        contextDb.Baskets.Add(Basket);
                        contextDb.SaveChanges();

                        Basket.BasketCars.Add(new BasketCar { BasketId = Basket.Id, CarId = car.Id });
                        contextDb.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Такої машини немає");
            }
            return View();
        }


    }

}
