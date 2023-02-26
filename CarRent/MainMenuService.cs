using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.CarRent.DataAccess.Models;
using CarRent.CarRent.DataAccess;
using Spectre.Console;

namespace CarRent
{
    public class MainMenuService
    {
        internal void MainMenu()
        {
            LogoDisplay();
            var option = OptionInput();
            SubMenuEntry(option, MainMenuContext());
        }

        internal static string OptionInput()
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orangered1]Choose corresponding menu:[/]")
                    .AddChoices(MainMenuContext()));
            return option;
        }

        internal static List<string> MainMenuContext()
        {
            var mainMenuContext = new List<string>
            {
                "Clients",
                "Cars",
                "Rentals",
                "Exit",
            };

            return mainMenuContext;
        }

        internal static void SubMenuEntry(string option, List<string> mainMenuContext)
        {
            switch (option)
            {
                case var m when m == mainMenuContext[0]:
                    var clientMenu = new ClientsMenuService();
                    clientMenu.ClientsMenu();
                    break;
                case var m when m == mainMenuContext[1]:
                    var carsMenu = new CarsMenuService();
                    carsMenu.CarsMenu();
                    break;
                case var m when m == mainMenuContext[2]:
                    var rentalsMenu = new RentalsMenuService();
                    rentalsMenu.RentalsMenu();
                    break;
                case var m when m == mainMenuContext[3]:
                    Environment.Exit(0);
                    break;
            }
        }

        internal static void LogoDisplay()
        {
            Console.Clear();
            AnsiConsole.Write(
                new FigletText("RentCar")
                    .Centered()
                    .Color(Color.Red));
        }
    }
}
