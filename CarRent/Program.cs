using CarRent.CarRent.DataAccess;
using CarRent.CarRent.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace CarRent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menuService = new MainMenuService();
            menuService.MainMenu();
        }
    }
}
