using CarShop.BL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.BL.ViewModel
{
    public class EditCarViewModel
    {
        public Car Car { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Engine Engine { get; set; }
        public SelectList Category { get; set; }
       public BasketCar BasketCar { get; set; }
        public Basket Basket { get; set; }

        public IEnumerable<CarLogo> CarLogos { get; set; }
        public Description Description { get; set; }
        public IEnumerable<Description> Descriptions { get; set; }

        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
