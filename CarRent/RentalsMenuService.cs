using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.CarRent.DataAccess;

namespace CarRent
{
    public class RentalsMenuService
    {
        internal void RentalsMenu()
        {
            var option = OptionInput();
            SubMenuEntry(option, RentalsMenuContext());
        }

        internal static string OptionInput()
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orangered1]Choose action:[/]")
                    .AddChoices(RentalsMenuContext()));
            return option;
        }

        internal static List<string> RentalsMenuContext()
        {
            var mainMenuContext = new List<string>();
            mainMenuContext.Add("Show all active rentals");
            mainMenuContext.Add("New car rental");
            mainMenuContext.Add("Finish active rental");
            mainMenuContext.Add("Show rental history");
            mainMenuContext.Add("Return");
            return mainMenuContext;
        }

        internal static void SubMenuEntry(string option, List<string> rentalsMenuContext)
        {
            var rentalsMenu = new RentalsMenuService();
            var mainMenuService = new MainMenuService();
            var rentalService = new RentalService(new RentalRepository());
            switch (option)
            {
                case var m when m == rentalsMenuContext[0]:
                    rentalService.DisplayAllActive();
                    break;
                case var m when m == rentalsMenuContext[1]:
                    rentalService.AddRental();
                    break;
                case var m when m == rentalsMenuContext[2]:
                    rentalService.finishRental();
                    break;
                case var m when m == rentalsMenuContext[3]:
                    rentalService.DisplayHistory();
                    break;
                case var m when m == rentalsMenuContext[4]:
                    mainMenuService.MainMenu();
                    break;
            }
            rentalsMenu.RentalsMenu();
        }
    }
}
