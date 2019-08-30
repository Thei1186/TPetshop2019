using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using TPetshop2019.Core.ApplicationServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.ShopConsole
{
    public class Printer: IPrinter
    {
        private readonly IPetService _petService;
        private readonly IOwnerService _ownerService;

        private static readonly string[] PetMenuItems = new string[]
            {"Create Pet", "Read Pet", "Update Pet", "Delete Pet", "List all Pets",
                "Search for pets", "Sort pets by price","Find the five cheapest Pets", "Exit to the Main Menu"};

        private static readonly string[] OwnerMenuItems = new string[]
            {"Create Owner", "Read Owner", "Update Owner", "Delete Owner", "List all Owners", "Exit to the Main Menu"};

        private static readonly string[] MainMenuItems = new string[]
            {"Pet functions", "Owner functions"};
        public Printer(IPetService petService, IOwnerService ownerService)
        {
            _petService = petService;
            _ownerService = ownerService;
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

        private string GetInput(string msg)
        {
            Console.WriteLine(msg + "\n");
            return Console.ReadLine();
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

        #region Owner methods
        public void OwnerChosenMenu()
        {
            var choice = 0;
            while (choice != OwnerMenuItems.Length)
            {
                ShowMenu(OwnerMenuItems);
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please write a number between 1 and " + OwnerMenuItems.Length);
                }

                switch (choice)
                {
                    case 1:
                        CreateOwner();
                        break;
                    case 2:
                        ReadOwner();
                        break;
                    case 3:
                        UpdateOwner();
                        break;
                    case 4:
                        DeleteOwner();
                        break;
                    case 5:
                        ListAllOwners();
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

        private void ListAllOwners()
        {
            foreach (var owner in _ownerService.ReadAllOwners())
            {
                PrintOwnerInfo(owner);
            }
        }

        private void DeleteOwner()
        {
            int id = Convert.ToInt32(GetInput("Write the id of the owner you want deleted"));
            this._ownerService.DeleteOwner(this._ownerService.ReadOwner(id));
        }

        private void UpdateOwner()
        {
            int id = Convert.ToInt32(GetInput("Write the id of the owner you want update"));
            string firstName = GetInput("Write the owner's first name");
            string lastName = GetInput("Write the owner's last name");
            string email = GetInput("Write the owner's email");
            string address = GetInput("Write the owner's address");
            string phoneNumber = GetInput("Write the owner's phone number");
            _ownerService.UpdateOwner(id, firstName, lastName, address, phoneNumber, email);
        }

        private void ReadOwner()
        {
            int id = Convert.ToInt32(GetInput("Write the id of the owner you want to view"));
            PrintOwnerInfo(this._ownerService.ReadOwner(id));
        }

        private void PrintOwnerInfo(Owner owner)
        {
            Console.Write(
                $"\nOwner found: \nId: {owner.Id}\nName: {owner.FirstName + " " + owner.LastName}\n" +
                $"Email: {owner.Email}\nAddress: {owner.Address}\n Colour: {owner.PhoneNumber}");
            Console.WriteLine("\n");
        }

        private void CreateOwner()
        {
            string firstName = GetInput("Write the owner's first name");
            string lastName = GetInput("Write the owner's last name");
            string email = GetInput("Write the owner's email");
            string address = GetInput("Write the owner's address");
            string phoneNumber = GetInput("Write the owner's phone number");
            this._ownerService.CreateOwner(firstName, lastName, address,phoneNumber, email);
        }

        #endregion


        #region Pet content
        public void PetChosenMenu()
        {
            var choice = 0;
            while (choice != PetMenuItems.Length)
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
                        SearchPets();
                        break;
                    case 7:
                        OrderPetListByPrice();
                        break;
                    case 8:
                        FindFiveCheapestPets();
                        break;
                    case 9:
                        ExitToMainMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid number.");
                        break;
                }
            }
        }

        public void OrderPetListByPrice()
        {
            var sortedPets = _petService.sortPets();
            foreach (var pet in sortedPets)
            {
                PrintPetInfo(pet);
            }
        }

        public void FindFiveCheapestPets()
        {
            var cheapestPets = _petService.GetFiveCheapestPets();
            foreach (var pet in cheapestPets)
            {
                PrintPetInfo(pet); 
            }
        }
        public void SearchPets()
        {
            var query = GetInput("Write the type of pet you want to search for");
            var searchResults = _petService.SearchPets(query);
            foreach (var pet in searchResults)
            {
                PrintPetInfo(pet);       
            }
        }

        public void PrintPetInfo(Pet pet)
        {
            Console.Write(
                $"\nPet found: \nId: {pet.Id}\nName: {pet.Name}\nType: {pet.Type}\nBirthdate: {pet.Birthdate}\n" +
                $"Colour: {pet.Colour}\nPreviousOwner: {getPreviousOwnerNameOrMsg(pet)}\n" +
                $"SoldDate: {pet.SoldDate}\nPrice: {pet.Price}\n");
            Console.WriteLine("\n");
        }
        private void ListAllPets()
        {
            Console.WriteLine("Listing all pets \n");
            foreach (var pet in this._petService.GetPets())
            {
               PrintPetInfo(pet);
            }

        }

        private void DeletePet()
        {
            int id;
            var query = GetInput("\nWrite the id of the pet you want to delete");
            while (!int.TryParse(query,out id))
            {
             Console.WriteLine("Please write a number");   
            }
            if (this._petService.ValidateId(id))
            {
                var petToDelete = this._petService.ReadPet(id);
                this._petService.DeletePet(petToDelete);
                Console.WriteLine($"The pet: {petToDelete.Name} has been deleted");
            }
            else
            {
                Console.WriteLine($"No pet with the id: {id} were found as such no pets were deleted\n" +
                                  $" Please verify the id you want to delete");
            }
        }

        public void UpdatePet()
        {
            var id = Convert.ToInt32(GetInput("Write the id of the pet you want to update"));
            var name = GetInput("Write the new name of the pet, or just the same one if no change is to be made");
            var colour = GetInput("Write the colour of the pet");
            var type = GetInput("Write the pet's type");
            var price = Convert.ToDouble(GetInput("Write the pet's price"));
            var birthdate = Convert.ToDateTime(GetInput("Write the pet's birthdate"));
            var soldDate = Convert.ToDateTime(GetInput("Write the pet's soldDate"));
            var previousOwner = new Owner();
            //PreviousOwner stuff is not yet made, therefore it is blank as it is needed in the update method
            this._petService.UpdatePet(id,name,type,colour,price,birthdate,soldDate,previousOwner);
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
               PrintPetInfo(pet);
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