using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.CarRent.DataAccess.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime SinceWhenRented { get; set; }
        public DateTime UntilWhenRented { get; set; }
        public DateTime? ReturnDate { get; set; } = null;
        public int? Cost { get; set; } = null;
    }
}
