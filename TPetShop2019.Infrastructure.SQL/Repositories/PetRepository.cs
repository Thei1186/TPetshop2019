using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.Infrastructure.SQL.Repositories
{
    public class PetRepository: IPetRepository
    {

        private PetShopContext _context;
        public PetRepository(PetShopContext context)
        {
            _context = context;
        }

        public FilteredList<Pet> ReadPets(Filter filter)
        {
            var filteredList = new FilteredList<Pet>();

            if (filter.CurrentPage > 0 && filter.ItemsPrPage > 0)
            {
                filteredList.List = _context.Pets
                    .Include(p => p.Colours)
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage).Take(filter.ItemsPrPage);

                filteredList.Count = _context.Pets.Count();
                return filteredList;
            }

            filteredList.List = _context.Pets
                .Include(p => p.Colours)
                .ToList();
            filteredList.Count = _context.Pets.Count();
            return filteredList;


        }

        public Pet AddPet(Pet pet)
        {
            _context.Attach(pet).State = EntityState.Added;
            _context.SaveChanges();
            return pet;
        }

        public Pet UpdatePet(Pet pet)
        {
            _context.Attach(pet).State = EntityState.Modified;
            _context.Entry(pet).Reference(p => p.PreviousOwner).IsModified = true;
            _context.SaveChanges();
            return pet;
        }

        public Pet RemovePet(int id)
        {
            var entityRemoved = _context.Remove(new Pet{PetId = id}).Entity;
            _context.SaveChanges();
            return entityRemoved;
        }

        public Pet GetSinglePetById(int id)
        {
            return _context.Pets.FirstOrDefault(pet => pet.PetId == id);
        }

        public Pet GetSinglePetByIdWithOwners(int id)
        {
            return _context.Pets
                .Include(p => p.Colours)
                .ThenInclude(c => c.Colour)
                .Include(p => p.PreviousOwner)
                .FirstOrDefault(p => p.PetId == id);
        }

        public int Count()
        {
            return _context.Pets.Count();
        }
    }
}