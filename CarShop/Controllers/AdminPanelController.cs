using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarShop.Models;
using Microsoft.EntityFrameworkCore;

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
            var cars = context.Cars.Include(x => x.Category).ToList();
            return View(cars);
        }
    }
}
