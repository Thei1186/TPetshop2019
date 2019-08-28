using System;
using TPetshop2019.Core.ApplicationServices;

namespace TPetshop2019.ShopConsole
{
    public class Printer
    {
        private IPetService _petService;
        private IOwnerService _ownerService;
        private static readonly string[] MenuItems = new string[]
            {"Create Pet", "Read Pet", "Update Pet", "Delete Pet", "List all Pets", "Exit"};

        public Printer(IPetService petService, IOwnerService ownerService)
        {
            _petService = petService;
            _ownerService = ownerService;
            ShowMenu();
        }

        public void ShowMenu()
        {
            //Console.Clear(); //Uncomment if you want console to clear between each choice

            for (int i = 0; i < MenuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + MenuItems[i]);
            }
        }
    }
}