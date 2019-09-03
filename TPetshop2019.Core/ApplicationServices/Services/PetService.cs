using System;
using System.Collections.Generic;
using System.Linq;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices.Services
{
    public class PetService : IPetService
    {
        private IPetRepository _petRepo;

        public PetService(IPetRepository petRepo)
        {
            this._petRepo = petRepo;
        }

        public Pet AddOwnerToPet(Pet newPet, Owner prevOwner)
        {
            newPet.PreviousOwner = prevOwner;
            return newPet;
        }

        public List<Pet> GetPets()
        {
            return this._petRepo.ReadPets().ToList();
        }

        public Pet NewPet(string name, string colour, string type, double price, DateTime birthDate)
        {
            Pet p1 = new Pet
                {
                    Birthdate = birthDate,
                    Colour = colour,
                    Name = name,
                    Price = price,
                    Type = type
                };
            return p1;
        }

        public Pet CreatePet(Pet pet)
        {
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
            return this._petRepo.ReadPets().FirstOrDefault(pet => pet.Id == id);
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
            }
            
            return _petRepo.UpdatePet(p1);
        }
        
        // New update method for the rest Api
        public Pet MakeUpdatedPet(Pet petToUpdate)
        {
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
            return this._petRepo.RemovePet(pet.Id);
        }

        public List<Pet> sortPets()
        {
            var listToSort = GetPets().OrderBy(pets => pets.Price).ToList();
            return listToSort;
        }
    }

}