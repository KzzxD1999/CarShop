using CarShop.BL.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CarShop.BL.ViewModel
{
    public class CarCategoryViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public User User { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}
