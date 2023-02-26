using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.CarRent.DataAccess;

namespace CarRent
{
    public class ClientsMenuService
    {
        internal void ClientsMenu()
        {
            var option = OptionInput();
            SubMenuEntry(option, ClientMenuContext());
        }

        internal static string OptionInput()
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orangered1]Choose action:[/]")
                    .AddChoices(ClientMenuContext()));
            return option;
        }

        internal static List<string> ClientMenuContext()
        {
            var mainMenuContext = new List<string>();
            mainMenuContext.Add("Show all clients");
            mainMenuContext.Add("Search for client");
            mainMenuContext.Add("Add new client");
            mainMenuContext.Add("Return");
            return mainMenuContext;
        }

        internal static void SubMenuEntry(string option, List<string> clientMenuContext)
        {
            var clientService = new ClientService(new ClientRepository());
            var mainMenuService = new MainMenuService();
            var clientMenu = new ClientsMenuService();
            switch (option)
            {
                case var m when m == clientMenuContext[0]:
                    clientService.DisplayAll();
                    break;
                case var m when m == clientMenuContext[1]:
                    clientService.SearchClient();
                    break;
                case var m when m == clientMenuContext[2]:
                    clientService.AddClient();
                    break;
                case var m when m == clientMenuContext[3]:
                    mainMenuService.MainMenu();
                    break;
            }
            SubMenuEntry(OptionInput(), ClientMenuContext());
        }
    }
}
