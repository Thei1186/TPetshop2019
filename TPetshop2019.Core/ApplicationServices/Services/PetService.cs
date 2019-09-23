using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepo;
        private readonly IValidateIdService _validateIdService;

        public PetService(IPetRepository petRepo, IValidateIdService validateIdService)
        {
            this._petRepo = petRepo;
            this._validateIdService = validateIdService;

        }

        public Pet AddOwnerToPet(Pet newPet, Owner prevOwner)
        {
            if (newPet == null || prevOwner == null)
            {
                throw new InvalidDataException("You need to specify an owner to add AND a pet to add the owner to");
            }
            newPet.PreviousOwner = prevOwner;
            return newPet;
        }

        public List<Pet> GetFilteredPets(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                throw  new InvalidDataException("CurrentPage and ItemsPage Must be zero or more");
            }
            if ((filter.CurrentPage - 1 * filter.ItemsPrPage) >= _petRepo.Count())
            {
                throw new InvalidDataException("Index out of bounds, CurrentPage is too high");
            }
            return _petRepo.ReadPets(filter).ToList();
        }

        public List<Pet> GetPets()
        {
            return this._petRepo.ReadPets().ToList();
        }

        //Old method for console
        public Pet NewPet(string name, string colour, string type, double price, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidDataException("The pet needs a name");
            }
            Pet p1 = new Pet
                {
                    Name = name,
                    Birthdate = birthDate,
                    Colour = colour,
                    Price = price,
                    Type = type
                };
            
            return CreatePet(p1);
        }

        //New method for RestApi
        public Pet CreatePet(Pet pet)
        {
            if (pet == null || pet.PreviousOwner.Id <= 0)
            {
                throw new InvalidDataException("Something went wrong when trying to create a pet. Please check the input");
            }
            return _petRepo.AddPet(pet);
        }

        public List<Pet> GetFiveCheapestPets()
        {
            var listToSort = GetPets().OrderBy(pets => pets.Price).ToList();
            if (listToSort.Count > 5)
            {
                var sortedList = new List<Pet>();

                for (int i = 0; i < 5; i++)
                {
                    sortedList.Add(listToSort[i]);
                }
                return sortedList;
            }

            return listToSort;
        }

        public List<Pet> SearchPets(string query)
        {
            var petMatchList = new List<Pet>();
            foreach (var pet in GetPets())
            {
                if (pet.Type.ToLower().Contains(query.ToLower()))
                {
                    petMatchList.Add(pet);
                }
            }
            return petMatchList;
        }

        public Pet ReadPet(int id)
        {
            if (!_validateIdService.ValidateId(id))
            {
                throw new InvalidDataException($"The id: {id} is invalid");
            }

            return _petRepo.GetSinglePetById(id);
        }

        public Pet ReadPetWithOwners(int id)
        {
            return _petRepo.GetSinglePetByIdWithOwners(id);
        }

        // Old update method
        public Pet UpdatePet(int id, string name, string type, string colour, double price,
            DateTime birthdate, DateTime soldDate, Owner previousOwner)
        {
            Pet p1 = ReadPet(id);

            if (p1 != null)
            {
                p1.Birthdate = birthdate;
                p1.Colour = colour;
                p1.Name = name;
                p1.PreviousOwner = previousOwner;
                p1.Price = price;
                p1.SoldDate = soldDate;
                p1.Type = type;
                return _petRepo.UpdatePet(p1);
            }
            return null;
        }
        
        // New update method for the rest Api
        public Pet MakeUpdatedPet(Pet petToUpdate)
        {
            if (petToUpdate == null)
            {
                throw new InvalidDataException("The pet received through the parameter was null");
            }
            var pet = ReadPet(petToUpdate.Id);
            pet.Name = petToUpdate.Name;
            pet.Birthdate = petToUpdate.Birthdate;
            pet.Colour = petToUpdate.Colour;
            pet.Price = petToUpdate.Price;
            pet.Type = petToUpdate.Type;
            pet.PreviousOwner = petToUpdate.PreviousOwner;
            pet.SoldDate = petToUpdate.SoldDate;
            
            return pet;
        }

        public Pet DeletePet(Pet pet)
        {
            if (pet == null)
            {
                throw new InvalidDataException("Pet was null so nothing was deleted");
            }
            return this._petRepo.RemovePet(pet.Id);
        }

        public List<Pet> sortPets()
        {
            var listToSort = GetPets().OrderBy(pets => pets.Price).ToList();
            return listToSort;
        }
    }

}