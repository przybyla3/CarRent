using CarRent.CarRent.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.CarRent.DataAccess.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spectre.Console;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Spectre.Console.Rendering;
using Table = Spectre.Console.Table;

namespace CarRent
{
    internal class CarService
    {
        private IRepository<Car> _carRepository;

        public CarService(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        internal void DisplayAll()
        {
            var headers = CreateTableHeaders("ALL CARS");
            var clients = _carRepository.GetAll();
            CreateFullTable(headers, clients);
        }


        internal void AddCar()
        {
            var car = new Car();
            car.Brand = ItemInput("Brand");
            car.Model = ItemInput("Model");
            car.Mileage = int.Parse(ItemInput("Mileage"));
            car.ProductionYear = int.Parse(ItemInput("Production Year"));
            car.Price = int.Parse(ItemInput("Price/day"));
            car.WhenServiced = int.Parse(ItemInput("Mileage When Serviced"));
            _carRepository.Add(car);
        }

        internal string ItemInput(string prompt)
        {
            var item = AnsiConsole.Ask<string>($"Enter [orangered1]{prompt}[/]?");
            return item;
        }

        internal Table CreateTableHeaders(string title)
        {
            var table = new Table();
            table.Title(title, new Style(Color.OrangeRed1));
            var names = typeof(Car).GetProperties()
                .Select(property => property.Name)
                .ToList();
            table.AddColumns(names.ToArray());
            return table;
        }
        internal void CreateFullTable(Table headers, List<Car> cars)
        {
            foreach (var car in cars)
            {
                headers.AddRow(car.CarId.ToString(), car.Brand, car.Model, car.ProductionYear.ToString(), car.Mileage.ToString(), car.IsRented.ToString(), car.Price.ToString(), car.WhenServiced.ToString());
            }
            AnsiConsole.Write(headers.LeftAligned());
        }
        internal List<Car> FindAvailableCars()
        {
            var cars = _carRepository.GetAll().Where(c => !c.IsRented).ToList();
            return cars;
        }

        internal Car CarPrompt(List<string> cars)
        {
            AnsiConsole.MarkupLine("[orangered1]Choose a car:[/]");
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orangered1]Id   Brand   Model  ProductionYear   Mileage   Price/day   WhenServiced[/]")!
                    .AddChoices(cars));
            var array = option.Split(' ');
            var carId = int.Parse(array.First());
            var car = _carRepository.GetById(carId);
            return car;
        }

        internal List<string> CarPromptContext(List<Car> cars)
        {
            var stringCars = new List<string>();
            foreach (var car in cars)
            {
                var stringCar = string.Join("   ", car.CarId, car.Brand, car.Model, car.ProductionYear, car.Mileage, car.Price, car.WhenServiced);
                stringCars.Add(stringCar);
            }
            return stringCars;
        }

        internal void ChangeRentedStatus(int id)
        {
            var car = _carRepository.GetById(id);
            car.IsRented = !car.IsRented;
            _carRepository.Update(car);
        }
    }
}


