﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarShop.Models;
using Microsoft.EntityFrameworkCore;
using CarShop.BL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using CarShop.BL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace CarShop.Controllers
{
    class Us{
            public string UserName { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
}
public class AdminPanelController : Controller
    {
        ContextDb context;


        public AdminPanelController(ContextDb _context, IConfiguration config)
        {
            context = _context;
           
        }
       
        [Authorize]
        public IActionResult Index()
        {

            User user = context.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            CarCategoryViewModel model = new CarCategoryViewModel
            {
                Cars = context.Cars.ToList(),
                Categories = context.Categories.ToList(),
                User = user,

            };
            //TODO: Chart.js
            return View(model);
        }

        

    }
}
