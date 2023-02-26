using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.CarRent.DataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace CarRent.CarRent.DataAccess
{
    public class CarRentDbContext : DbContext
    {
        public DbSet<Client> Clients {get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"Data Source=DESKTOP-HBKL8T7;Initial Catalog=CarRentDb;Integrated Security=true;encrypt=false;");
    }
}

