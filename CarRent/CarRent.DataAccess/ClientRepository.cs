using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.CarRent.DataAccess.Models;

namespace CarRent.CarRent.DataAccess
{
    internal class ClientRepository : IRepository<Client>
    {
        private CarRentDbContext _dbContext;

        public ClientRepository()
        {
            _dbContext = new CarRentDbContext();
        }

        public Client GetById(int id)
        {
            return _dbContext.Clients.Single(e => e.ClientId == id);

        }

        public Client? GetByName(string name)
        {
            return _dbContext.Clients.Single(e => e.LastName == name);
        }
        public Client? GetByFirstName(string name)
        {
            return _dbContext.Clients.Single(e => e.FirstName == name);
        }

        public List<Client> GetAll()
        {
            return _dbContext.Clients.ToList();
        }

        public void Add(Client client)
        {
            _dbContext.Add(client);
            _dbContext.SaveChanges();
        }

        public void Delete(Client client)
        {
            _dbContext.Remove(client);
            _dbContext.SaveChanges();
        }

        public void Update(Client client)
        {
            _dbContext.Update(client);
            _dbContext.SaveChanges();
        }
    }
}
