using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.CarRent.DataAccess
{

    public interface IRepository<T> where T : class
    {
        public T GetById(int id);

        public T? GetByName(string name);

        public List<T> GetAll();

        public void Add(T item);

        public void Delete(T client);

        public void Update(T item);
    }
    
}

