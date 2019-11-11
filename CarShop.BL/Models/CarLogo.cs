using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.BL.Models
{
    public class CarLogo
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Назва компанії обов'язкова")]
        public string NameLogo { get; set; }
        
        public string Path { get; set; }
        public List<Car> Cars { get; set; }
    }
}
