using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachine.Models
{
    public class DrinkContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }

        public DrinkContext(DbContextOptions<DrinkContext> options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
