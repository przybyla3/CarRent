using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.CarRent.DataAccess;

namespace CarRent
{
    public class CarsMenuService
    {
        internal void CarsMenu()
        {
            var carService = new CarService(new CarRepository());
            MainMenuService.LogoDisplay();
            carService.DisplayAll();
            var option = OptionInput();
            SubMenuEntry(option, CarsMenuContext());
            
        }

        internal static string OptionInput()
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orangered1]Choose action:[/]")
                    .AddChoices(CarsMenuContext()));
            return option;
        }

        internal static List<string> CarsMenuContext()
        {
            var mainMenuContext = new List<string>();
            mainMenuContext.Add("Add new car");
            mainMenuContext.Add("Return");
            return mainMenuContext;
        }

        internal static void SubMenuEntry(string option, List<string> carsMenuContext)
        {
            var carService = new CarService(new CarRepository());
            var mainMenu = new MainMenuService();
            var carMenu = new CarsMenuService();
            switch (option)
            {
                case var m when m == carsMenuContext[0]:
                    carService.AddCar();
                    break;
                case var m when m == carsMenuContext[1]:
                    mainMenu.MainMenu();
                    break;
            }
            carMenu.CarsMenu();
        }
    }
}
