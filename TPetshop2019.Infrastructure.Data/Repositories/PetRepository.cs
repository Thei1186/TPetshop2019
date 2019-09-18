using System.Collections.Generic;
using System.Linq;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Infrastructure.Data.Repositories
{
    public class PetRepository
    {
        public IEnumerable<Pet> ReadPets()
        {
            return FakeDB.PetTable;
        }

        public Pet AddPet(Pet pet)
        {
            pet.Id = FakeDB.PetId++;
            var pets = FakeDB.PetTable.ToList();
            pets.Add(pet);
            FakeDB.PetTable = pets;
            return pet;
        }

        public Pet UpdatePet(Pet pet)
        {
            var pets = FakeDB.PetTable.ToList();
            var chosenPet = pets.FirstOrDefault(p => p.Id == pet.Id);
            if (chosenPet != null)
            {
                pets.Remove(chosenPet);
                pets.Add(pet);
            }

            FakeDB.PetTable = pets;

            return pet;
        }

        public Pet RemovePet(int id)
        {
            var pets = FakeDB.PetTable.ToList();
            var chosenPet = pets.FirstOrDefault(pet => pet.Id == id);
            pets.Remove(chosenPet);
            FakeDB.PetTable = pets;
            return chosenPet;
        }

        public Pet GetSinglePetById(int id)
        {
            return FakeDB.PetTable.FirstOrDefault(pet => pet.Id == id);
        }
    }
}