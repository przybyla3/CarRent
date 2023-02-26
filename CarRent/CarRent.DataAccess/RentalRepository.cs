using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.CarRent.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRent.CarRent.DataAccess
{
    internal class RentalRepository : IRepository<Rental>
    {
        private CarRentDbContext _dbContext;

        public RentalRepository()
        {
            _dbContext = new CarRentDbContext();
        }

        public Rental GetById(int id)
        {
            return _dbContext.Rentals.Single(e => e.RentalId == id);

        }

        public Rental? GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Rental> GetAll()
        {
            return _dbContext.Rentals.Include(x => x.Car).Include(x => x.Client).ToList();
        }

        public void Add(Rental rental)
        {
            _dbContext.Add(rental);
            _dbContext.SaveChanges();
        }

        public void Delete(Rental rental)
        {
            _dbContext.Remove(rental);
            _dbContext.SaveChanges();
        }

        public void Update(Rental rental)
        {
            _dbContext.Update(rental);
            _dbContext.SaveChanges();
        }
    }
}