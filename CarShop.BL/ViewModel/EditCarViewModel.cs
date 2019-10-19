using CarShop.BL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.BL.ViewModel
{
    public class EditCarViewModel
    {
        public Car Car { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Engine Engine { get; set; }
        public SelectList Category { get; set; }
    }
}
