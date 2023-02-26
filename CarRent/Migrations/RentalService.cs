using CarRent.CarRent.DataAccess;
using CarRent.CarRent.DataAccess.Models;
using Spectre.Console;
using Table = Spectre.Console.Table;


namespace CarRent
{
    internal class RentalService
    {
        private IRepository<Rental> _rentalRepository;

        public RentalService(IRepository<Rental> rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        internal void DisplayAllActive()
        {
            MainMenuService.LogoDisplay();
            var headers = CreateTableHeaders("ALL ACTIVE RENTALS");
            var rentals = _rentalRepository.GetAll().Where(c => c.ReturnDate == null).ToList();
            CreateFullTable(headers, rentals);
        }


        internal void AddRental()
        {
            var rental = new Rental();
            var client = gettinClient();
            Console.WriteLine("{0} {1}", client.FirstName, client.LastName);
            var car = gettinCar();
            Console.WriteLine("{0} {1}", car.Brand, car.Model);
            rental.ClientId = client.ClientId;
            rental.CarId = car.CarId;
            rental.SinceWhenRented = DateTime.Parse(ItemInput("Starting Date (dd/mm/yyyy)"));
            rental.UntilWhenRented = DateTime.Parse(ItemInput("Ending Date (dd/mm/yyyy)"));
            rental.ReturnDate = null;
            var carService = new CarService(new CarRepository());
            carService.ChangeRentedStatus(rental.CarId);
            _rentalRepository.Add(rental);
        }

        internal void finishRental()
        {
            var rentals = rentalPromptContext();
            var rental = rentalPrompt(rentals);
            rental.ReturnDate = DateTime.Parse(ItemInput("Return Date (dd/mm/yyyy)"));
            var carService = new CarService(new CarRepository());
            carService.ChangeRentedStatus(rental.CarId);
            _rentalRepository.Update(rental);
            MainMenuService.LogoDisplay();
        }

        internal string ItemInput(string prompt)
        {
            var item = AnsiConsole.Ask<string>($"Enter [orangered1]{prompt}[/]?");
            return item;
        }
        internal void DisplayHistory()
        {
            MainMenuService.LogoDisplay();
            var headers = CreateTableHeaders("ALL RENTALS");
            var rentals = _rentalRepository.GetAll().Where(c => c.ReturnDate != null).ToList();
            CreateFullTable(headers, rentals);
        }

        internal Table CreateTableHeaders(string title)
        {
            var table = new Table();
            table.Title(title, new Style(Color.OrangeRed1));
            var names = typeof(Rental).GetProperties()
                .Select(property => property.Name)
                .ToList();
            table.AddColumns(names.ToArray());
            return table;
        }

        internal void CreateFullTable(Table headers, List<Rental> rentals)
        {
            var cars = new CarRepository();
            var clients = new ClientRepository();

            foreach (var rental in rentals)
            {
                ////var client = clients.GetById(rental.ClientId);
                ////var car = cars.GetById(rental.CarId);
                headers.AddRow(rental.RentalId.ToString(), rental.CarId.ToString(), rental.Car.Model, rental.ClientId.ToString(), rental.Client.LastName, rental.SinceWhenRented.ToString(), rental.UntilWhenRented.ToString(), rental.ReturnDate.ToString(), rental.Cost.ToString());
            }

            AnsiConsole.Write(headers.LeftAligned());
        }

        internal Client gettinClient()
        {
            var clientService = new ClientService(new ClientRepository());
            var clients = clientService.inputClientsForPrompt();
            var client = clientService.clientPrompt(clientService.clientPromptContext(clients));
            return client;
        }
        internal Car gettinCar()
        {
            var carService = new CarService(new CarRepository());
            var cars = carService.FindAvailableCars();
            var car = carService.CarPrompt(carService.CarPromptContext(cars));
            return car;
        }
        internal List<string> rentalPromptContext()
        {
            var cars = new CarRepository();
            var clients = new ClientRepository();
            var rentals = _rentalRepository.GetAll().Where(c => c.ReturnDate == null).ToList();
            var stringRentals = new List<string>();
            foreach (var rental in rentals)
            {
                var client = clients.GetById(rental.ClientId);
                var car = cars.GetById(rental.CarId);
                var stringClient = string.Join("   ", rental.RentalId.ToString(), rental.CarId.ToString(), car.Model, rental.ClientId.ToString(), client.LastName, rental.SinceWhenRented.ToString(), rental.UntilWhenRented.ToString(), "Not Returned");
                stringRentals.Add(stringClient);
            }
            return stringRentals;
        }
        internal Rental rentalPrompt(List<string> rentals)
        {
            AnsiConsole.MarkupLine("[orangered1]Choose the client:[/]");
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orangered1]Id  CarId  Car  ClientId  LastName  StartingDate  FinishDate  Returned [/]")!
                    .AddChoices(rentals));
            var array = option.Split(' ');
            var rentalId = int.Parse(array.First());
            var rental = _rentalRepository.GetById(rentalId);
            return rental;
        }

        internal int howLong(Rental rental)
        {
            var days = (rental.ReturnDate.Value - rental.SinceWhenRented).Days;
            return days;
        }

        internal void settingCost(Rental rental, int id)
        {
            var carRepository = new CarRepository();
            var car = carRepository.GetById(id);
            var days = howLong(rental);
            rental.Cost = car.Price * days;
            _rentalRepository.Update(rental);
        }



    }
}


