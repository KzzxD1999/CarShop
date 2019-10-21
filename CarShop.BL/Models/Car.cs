using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CarShop.BL.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Price { get; set; }
        public double MaxSpeed { get; set; }
        public int Color { get; set; }
        public string ImagePath { get; set; }
        public string BigImagePath { get; set; }
        public int EngineId { get; set; }
        public Engine Engine { get; set; }
        public int Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    
    }
}
