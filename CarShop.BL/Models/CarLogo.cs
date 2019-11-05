using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.BL.Models
{
    public class CarLogo
    {

        public int Id { get; set; }

        public string NameLogo { get; set; }
        public string Path { get; set; }
        public List<Car> Cars { get; set; }
    }
}
