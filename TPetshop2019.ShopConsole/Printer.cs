using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using TPetshop2019.Core.ApplicationServices;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.ShopConsole
{
    public class Printer: IPrinter
    {
        private readonly IPetService _petService;
        private readonly IOwnerService _ownerService;
        private readonly IValidateIdService _validationService;

        private static readonly string[] PetMenuItems = new string[]
            {"Create Pet", "Read Pet", "Update Pet", "Delete Pet", "List all Pets",
                "Search for pets", "Sort pets by price","Find the five cheapest Pets", "Exit to the Main Menu"};

        private static readonly string[] OwnerMenuItems = new string[]
            {"Create Owner", "Read Owner", "Update Owner", "Delete Owner", "List all Owners", "Exit to the Main Menu"};

        private static readonly string[] MainMenuItems = new string[]
            {"Pet functions", "Owner functions"};
        public Printer(IPetService petService, IOwnerService ownerService, IValidateIdService validationService)
        {
            _petService = petService;
            _ownerService = ownerService;
            _validationService = validationService;
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
            var owners = this._ownerService.ReadAllOwners();
            if (owners.Count > 0)
            {
                foreach (var owner in owners)
                {
                    PrintOwnerInfo(owner);
                }
            }
            else
            {
                Console.WriteLine("No owners were found");
            }
        }

        private void DeleteOwner()
        {
            int id;
            var query = GetInput("\nWrite the id of the owner you want to delete");
            while (!int.TryParse(query, out id))
            {
                Console.WriteLine("Please write a number");
            }
            if (this._validationService.ValidateId(id))
            {
                var ownerToDelete = this._ownerService.DeleteOwner(this._ownerService.ReadOwner(id));
                Console.WriteLine($"The owner: {ownerToDelete.FirstName + " " + ownerToDelete.LastName} has been deleted");
            }
            else
            {
                Console.WriteLine($"No owner with the id: {id} were found as such no owners were deleted\n" +
                                  $" Please verify the id of the owner, that you want to delete");
            }
        }

        private void UpdateOwner()
        {
            int id = Convert.ToInt32(GetInput("Write the id of the owner you want update\n"));
            if (this._validationService.ValidateId(id))
            {
                string firstName = GetInput("Write the owner's first name");
                string lastName = GetInput("Write the owner's last name");
                string email = GetInput("Write the owner's email");
                string address = GetInput("Write the owner's address");
                string phoneNumber = GetInput("Write the owner's phone number\n");
                _ownerService.UpdateOwner(id, firstName, lastName, address, phoneNumber, email);
            }
            else
            {
                Console.WriteLine("PetId not valid, exiting method");
            }
        }

        private void ReadOwner()
        {
            int id = Convert.ToInt32(GetInput("Write the id of the owner you want to view"));
            if (this._validationService.ValidateId(id))
            {
                PrintOwnerInfo(this._ownerService.ReadOwner(id));
            }
            else
            {
                Console.WriteLine("PetId not valid, exiting method");
            }
        }

        private void PrintOwnerInfo(Owner owner)
        {
            Console.Write(
                $"\nOwner found: \nId: {owner.Id}\nName: {owner.FirstName + " " + owner.LastName}\n" +
                $"Email: {owner.Email}\nAddress: {owner.Address}\nPhone number: {owner.PhoneNumber}");
            Console.WriteLine("\n");
        }

        private Owner CreateOwner()
        {
            string firstName = GetInput("Write the owner's first name");
            string lastName = GetInput("Write the owner's last name");
            string email = GetInput("Write the owner's email");
            string address = GetInput("Write the owner's address");
            string phoneNumber = GetInput("Write the owner's phone number");
            Console.WriteLine("\n");
            return this._ownerService.NewOwner(firstName, lastName, address,phoneNumber, email);
        }

        #endregion


        #region Pet methods
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
            if (searchResults != null)
            {
                foreach (var pet in searchResults)
                {
                    PrintPetInfo(pet);
                }
            }
            else
            {
                Console.WriteLine("No results for the query: {0}", query);
            }

        }

        public void PrintPetInfo(Pet pet)
        {
            Console.Write(
                $"\nPet found: \nId: {pet.PetId}\nName: {pet.Name}\nType: {pet.Type}\nBirthdate: {pet.Birthdate}\n" +
                $"Colour: {pet.Colour}\nPreviousOwner: {getPreviousOwnerNameOrMsg(pet)}\n" +
                $"SoldDate: {pet.SoldDate}\nPrice: {pet.Price}\n");
            Console.WriteLine("\n");
        }
        private void ListAllPets()
        {
            Console.WriteLine("Listing all pets \n");
            var petList = this._petService.GetPets();
            if (petList != null)
            {
                foreach (var pet in petList)
                {
                 PrintPetInfo(pet);
                }
            }
            else
            {
                Console.WriteLine("There are currently no pets in the database list");
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
            if (this._validationService.ValidateId(id))
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
            var previousOwner = CreateOwner();
            
            this._petService.UpdatePet(id,name,type,colour,price,birthdate,soldDate,previousOwner);
            Console.WriteLine($"The pet with PetId: {id} has been updated\n");
        }

        private void ReadPet()
        {
            Console.WriteLine("Write the id of the wanted pet");
            int searchId;
            while (!int.TryParse(Console.ReadLine(), out searchId))
            {
                Console.WriteLine("Please write a number above 0");
            }

            if (this._validationService.ValidateId(searchId))
            {
               Pet pet = this._petService.ReadPet(searchId);
               PrintPetInfo(pet);
            }
        }

        private string getPreviousOwnerNameOrMsg(Pet pet)
        {
            if (pet.PreviousOwner != null)
            {
                var ownerName = pet.PreviousOwner.FirstName + " " + pet.PreviousOwner.LastName;
                return ownerName;
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
            var newPet = this._petService.NewPet(name, colour, type, price, birthdate);

            if (ValidateChoice("Do you want to add a previous owner for the new pet?"))
            {
                var previousOwner = CreateOwner();
                this._petService.AddOwnerToPet(newPet, previousOwner);
            }

            Console.WriteLine($"The pet named: {newPet.Name} has been created");
        }
        #endregion

        #region Misc methods
        private bool ValidateChoice(string msg)
        {
            Console.WriteLine(msg + "\n" + "press Y for yes or N for no");
            var choice = Console.ReadLine();
            if (choice == null || !choice.ToLower().Equals("y") && !choice.ToLower().Equals("n"))
            {
                Console.WriteLine("Invalid input, returning to menu");
            }
            else if (choice.ToLower().Equals("y"))
            {
                return true;
            }

            return false;
        }

        public void ChooseMenu()
        {
            Console.WriteLine("Choose which functions to access");
            ShowMenu(MainMenuItems);
            int choice;
            while (!int.TryParse(Console.ReadLine(),out choice))
            {
                Console.WriteLine("Please write a number");
            }
            switch (choice)
            {
                case 1:
                    PetChosenMenu();
                    break;
                case 2:
                    OwnerChosenMenu();
                    break;
                default:
                    Console.WriteLine("\nPlease write either 1 or 2\n");
                    ChooseMenu();
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
        #endregion

    }
}