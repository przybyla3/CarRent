using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.CarRent.DataAccess.Models;

namespace CarRent.CarRent.DataAccess
{
    internal class CarRepository : IRepository<Car>
    {
        private CarRentDbContext _dbContext;

        public CarRepository()
        {
            _dbContext = new CarRentDbContext();
        }

        public Car GetById(int id)
        {
            return _dbContext.Cars.Single(e => e.CarId == id);

        }

        public Car? GetByName(string name)
        {
            return _dbContext.Cars.Single(e => e.Brand == name);
        }
        public Car? GetByFirstName(string name)
        {
            return _dbContext.Cars.Single(e => e.Model == name);
        }

        public List<Car> GetAll()
        {
            return _dbContext.Cars.ToList();
        }

        public void Add(Car car)
        {
            _dbContext.Add(car);
            _dbContext.SaveChanges();
        }

        public void Delete(Car car)
        {
            _dbContext.Remove(car);
            _dbContext.SaveChanges();
        }

        public void Update(Car car)
        {
            _dbContext.Update(car);
            _dbContext.SaveChanges();
        }
    }
}