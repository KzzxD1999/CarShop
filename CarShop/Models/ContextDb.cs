using CarShop.BL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class ContextDb : IdentityDbContext<User>
    {
        public ContextDb(DbContextOptions<ContextDb> x) : base(x)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Engine> Engines { get; set; }
        //public DbSet<Basket> Baskets { get; set; }

    }
}
