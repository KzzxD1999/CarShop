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
        [Required(ErrorMessage ="Назва машини обов'язкова")]
        [StringLength(20, MinimumLength = 2, ErrorMessage ="Довжина назви повинна бути не меншою за 2 і не більшою за 20")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Опис обов'язковий")]
        public string Text { get; set; }

        [Required(ErrorMessage ="Поле Категорія обов'язкве")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required(ErrorMessage ="Ціна обов'язкова")]
        [Range(1000, 9999999, ErrorMessage ="Ціна повинна бути в межах від 1000 до 9999999")]
        public int Price { get; set; }
        [Required(ErrorMessage ="Поле максимальна швидкість обов'язкова")]
        public double MaxSpeed { get; set; }
        public int Color { get; set; }
        public string ImagePath { get; set; }
        public string BigImagePath { get; set; }
        [Required(ErrorMessage = "Поле мотор обовя'зковие")]
        public int EngineId { get; set; }
        public Engine Engine { get; set; }

        [Required(ErrorMessage = "Поле Вага є обов'язковим")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Поле Висота є обов'язковим")]
        public double Height { get; set; }

        [Required(ErrorMessage = "Поле Ширина є обов'язковим")]
        public double Width { get; set; }


        //  public int BasketId { get; set; }
        //  public Basket Basket { get; set; }

        public int CarLogoId { get; set; }
        public CarLogo CarLogo { get; set; }
        public List<BasketCar> BasketCars { get; set; }

       public List<Description> Descriptions { get; set; }


        public Car()
        {
            BasketCars = new List<BasketCar>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
