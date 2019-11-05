using CarShop.BL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShop.BL.ViewModel
{
    public class CarsViewModel
    {
        public IEnumerable<Car> Cars { get; set; }

        public SelectList Categories { get; set; }

        public List<BasketCar> Basket { get; set; }
   
    }
}
