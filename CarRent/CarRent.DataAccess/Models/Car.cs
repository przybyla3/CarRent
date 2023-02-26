using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CarRent.CarRent.DataAccess.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Brand{ get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
        public bool IsRented { get; set; } = false;
        public int Price { get; set; }
        public int WhenServiced { get; set; }
    }
}
