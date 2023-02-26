using CarRent.CarRent.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    internal class ClientService
    {
        private IRepository<Client> _clientRepository;

        public ClientService(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        internal void DisplayAll()
        {
            var clientsMenu = new ClientsMenuService();
            MainMenuService.LogoDisplay();
            var headers = CreateTableHeaders("ALL CLIENTS");
            var clients = _clientRepository.GetAll();
            CreateFullTable(headers,clients);
            clientsMenu.ClientsMenu();
        }

        internal void SearchClient()
        {
            var name = ItemInput("Last Name");
            var client = _clientRepository.GetAll().Where(c=>c.LastName.Contains(name));
            var headers = CreateTableHeaders("SEARCH RESULT CLIENTS");
            MainMenuService.LogoDisplay();
            CreateFullTable(headers, client.ToList());
        }
        internal void AddClient()
        {
            var client = new Client();
            client.FirstName = ItemInput("First Name");
            client.LastName = ItemInput("Last Name");
            client.PhoneNumber = int.Parse(ItemInput("Phone Number"));
            _clientRepository.Add(client);
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
            var names = typeof(Client).GetProperties()
                .Select(property => property.Name)
                .ToList();
            table.AddColumns(names.ToArray());
            return table;
        }
        internal void CreateFullTable(Table headers, List<Client> clients)
        {
            foreach (var name in clients)
            {
                headers.AddRow(name.ClientId.ToString(), name.FirstName, name.LastName, name.PhoneNumber.ToString());
            }
            AnsiConsole.Write(headers.LeftAligned());
        }
        internal List<Client> inputClientsForPrompt()
        {
            var lastName = ItemInput("Last Name");
            var clients = _clientRepository.GetAll().Where(c => c.LastName == lastName).ToList();
            return clients;
        }
        internal List<string> clientPromptContext(List<Client> clients)
        {
            var stringClients = new List<string>();
            foreach (var client in clients)
            {
                var stringClient = string.Join("   ", client.ClientId, client.FirstName, client.LastName, client.PhoneNumber);
                stringClients.Add(stringClient);
            }
            return stringClients;
        }

        internal Client clientPrompt(List<string> clients)
        {
            AnsiConsole.MarkupLine("[orangered1]Choose the client:[/]");
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orangered1]Id  FirstName  LastName  PhoneNumber[/]")!
                    .AddChoices(clients));
            var array = option.Split(' ');
            var clientID = int.Parse(array.First());
            var client = _clientRepository.GetById(clientID);
            return client;
        }

        

    }
}


