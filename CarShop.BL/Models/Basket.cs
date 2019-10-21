using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.BL.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public List<Car> Cars { get; set; }
    }
}
