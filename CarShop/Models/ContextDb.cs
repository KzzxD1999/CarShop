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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
               .HasOne(p => p.Basket)
               .WithOne(i => i.User)
               .HasForeignKey<Basket>(b => b.UserId);
            base.OnModelCreating(builder);

            builder.Entity<BasketCar>()
                .HasKey(t => new { t.BasketId, t.CarId });
            builder.Entity<BasketCar>()
                .HasOne(bc => bc.Basket)
                .WithMany(s => s.BasketCars)
                .HasForeignKey(bc => bc.BasketId);

            builder.Entity<BasketCar>()
                .HasOne(bc => bc.Car)
                .WithMany(s => s.BasketCars)
                .HasForeignKey(bc => bc.CarId);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketCar> BasketCars { get; set; }
    }
}
