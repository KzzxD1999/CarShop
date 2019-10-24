using CarShop.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.BL.ViewModel
{
    public class UserBasketViewModel
    {

        public IEnumerable<Basket> Baskets { get; set; }
        public List<Car> Cars { get; set; }
    }
}
