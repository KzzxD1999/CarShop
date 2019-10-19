using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarShop.Models;
using Microsoft.EntityFrameworkCore;
using CarShop.BL.ViewModel;

namespace CarShop.Controllers
{
    public class AdminPanelController : Controller
    {
        ContextDb context;

        public AdminPanelController(ContextDb _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            CarCategoryViewModel model = new CarCategoryViewModel
            {
                Cars = context.Cars.ToList(),
                Categories = context.Categories.ToList(),

            };
            return View(model);
        }

    }
}
