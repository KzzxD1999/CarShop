using System.ComponentModel.DataAnnotations;

namespace CarShop.BL.Models
{
    public class Engine
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Назва мотору є обов'язковим")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Кіньські сили є обов'язковим")]
        public int Horsepower { get; set; }
    }
}