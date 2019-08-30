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

        public List<Pet> GetPets()
        {
            return this._petRepo.ReadPets().ToList();
        }

        public Pet CreatePet(string name, string colour, string type, double price, DateTime birthDate)
        {
            Pet p1 = new Pet
                {
                    Birthdate = birthDate,
                    Colour = colour,
                    Name = name,
                    Price = price,
                    Type = type
                };
            return this._petRepo.AddPet(p1);
        }

        public List<Pet> GetFiveCheapestPets()
        {
            var listToSort = GetPets().OrderBy(pets => pets.Price).ToList();
            if (listToSort.Count > 5)
            {
                var sortedList = new List<Pet>();

                for (int i = 0; i < 4; i++)
                {
                    sortedList[i] = listToSort[i];
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


        public bool ValidateId(int id)
        {
                if (id > 0 )
                {
                    return true;
                }

                return false;
        }

        public Pet ReadPet(int id)
        {
            return this._petRepo.ReadPets().FirstOrDefault(pet => pet.Id == id);
        }

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
            
            return this._petRepo.UpdatePet(p1);
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