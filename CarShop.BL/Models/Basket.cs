using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.BL.Models
{
    public class Basket
    {
        public int Id { get; set; }


        public string NameBasket { get; set; }
        //public List<Car> Cars { get; set; }
        public List<BasketCar> BasketCars { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }


        public Basket()
        {
            BasketCars = new List<BasketCar>();
        }


    }
}
