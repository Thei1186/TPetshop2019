using System;
using System.Collections.Generic;
using System.Diagnostics;
using TPetshop2019.Core.ApplicationServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.ShopConsole
{
    public class Printer
    {
        private readonly IPetService _petService;
        private readonly IOwnerService _ownerService;

        private static readonly string[] PetMenuItems = new string[]
            {"Create Pet", "Read Pet", "Update Pet", "Delete Pet", "List all Pets", "Exit to the Main Menu"};

        private static readonly string[] OwnerMenuItems = new string[]
            {"Create Owner", "Read Owner", "Update Owner", "Delete Owner", "List all Owners", "Exit to the Main Menu"};

        private static readonly string[] MainMenuItems = new string[]
            {"Pet functions", "Owner functions"};
        public Printer(IPetService petService, IOwnerService ownerService)
        {
            _petService = petService;
            _ownerService = ownerService;
            ChooseMenu();
        }

        public void ChooseMenu()
        {
            ShowMenu(MainMenuItems);
            var choice = Convert.ToInt32(GetInput("Choose which functions to access"));
            switch (choice)
            {
                case 1:
                    PetChosenMenu();
                    break;
                case 2:
                    OwnerChosenMenu();
                    break;
                default:
                    break;
            }

        }

        public void ShowMenu(string[] menuItems)
        {
            //Console.Clear(); //Uncomment if you want console to clear between each choice

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + menuItems[i]);
            }
        }
        private void ExitToMainMenu()
        {
            Console.Clear();
            ChooseMenu();
        }

        public void OwnerChosenMenu()
        {
            var choice = 0;
            while (choice != 6)
            {
                ShowMenu(OwnerMenuItems);
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please write a number between 1 and " + OwnerMenuItems.Length);
                }

                switch (choice)
                {
                    case 1:
                        CreatePet();
                        break;
                    case 2:
                        ReadPet();
                        break;
                    case 3:
                        UpdatePet();
                        break;
                    case 4:
                        DeletePet();
                        break;
                    case 5:
                        ListAllPets();
                        break;
                    case 6:
                        ExitToMainMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid number.");
                        break;
                }
            }
        }

        #region Pet content
        public void PetChosenMenu()
        {
            var choice = 0;
            while (choice != 6)
            {
                ShowMenu(PetMenuItems);
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please write a number between 1 and " + PetMenuItems.Length);
                }

                switch (choice)
                {
                    case 1:
                        CreatePet();
                        break;
                    case 2:
                        ReadPet();
                        break;
                    case 3:
                        UpdatePet();
                        break;
                    case 4:
                        DeletePet();
                        break;
                    case 5:
                        ListAllPets();
                        break;
                    case 6:
                        ExitToMainMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid number.");
                        break;
                }
            }
        }


        private void ListAllPets()
        {
            Console.WriteLine("Listing all pets \n");
            foreach (var pet in this._petService.GetPets())
            {
                Console.Write(
                    $"Pet found: \nId: {pet.Id}\nName: {pet.Name}\nType: {pet.Type}\nBirthdate: {pet.Birthdate}\n" +
                    $"Colour: {pet.Colour}\nPreviousOwner: {getPreviousOwnerNameOrMsg(pet)}\n" +
                    $"SoldDate: {pet.SoldDate}\nPrice: {pet.Price}\n");
            }

            Console.WriteLine("\n");
        }

        private void DeletePet()
        {
            int id = ValidateIntInputAndReturnIt();
            var petToDelete = this._petService.ReadPet(id); 
            this._petService.DeletePet(petToDelete);
        }

        private int ValidateIntInputAndReturnIt()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Please write a number");
                while (!this._petService.ValidateId(input))
                {
                    Console.WriteLine("Incorrect, please write an id above 0");
                }
            }
            return input;
        }
        
        private void UpdatePet()
        {
            throw new NotImplementedException();
        }

        private void ReadPet()
        {
            Console.WriteLine("Write the id of the wanted pet");
            int searchId;
            while (!int.TryParse(Console.ReadLine(), out searchId))
            {
                Console.WriteLine("Please write a number above 0");
            }

            if (searchId != 0)
            {
               Pet pet = this._petService.ReadPet(searchId);
               Console.Write($"Pet found: Id: {pet.Id}\nName: {pet.Name}\nType: {pet.Type}\nBirthdate: {pet.Birthdate}\n" + 
                             $"Colour: {pet.Colour}\nPreviousOwner: {getPreviousOwnerNameOrMsg(pet)}\n" +
                             $"SoldDate: {pet.SoldDate}\nPrice: {pet.Price}\n");
            }
        }

        private string getPreviousOwnerNameOrMsg(Pet pet)
        {
            if (pet.PreviousOwner != null)
            {
                return pet.PreviousOwner.FirstName + " " + pet.PreviousOwner.LastName;
            }

            return "No Previous Owner";
        }
        private string GetInput(string msg)
        {
            Console.WriteLine(msg + "\n");
            return Console.ReadLine();
        }
        private void CreatePet()
        {
            var name = GetInput("Write a name for the pet");
            var colour = GetInput("Write the colour of the pet");
            var type = GetInput("Write the pet's type");
            var price = Convert.ToDouble(GetInput("Write the pet's price"));
            var birthdate = Convert.ToDateTime(GetInput("Write the pet's birthdate"));

            this._petService.CreatePet(name, colour, type, price, birthdate);
        }
        #endregion

    }
}