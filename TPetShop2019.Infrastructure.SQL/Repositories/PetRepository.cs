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

        public IEnumerable<Pet> ReadPets()
        {
            return _context.Pets.ToList();
        }

        public Pet AddPet(Pet pet)
        {
            _context.Attach(pet).State = EntityState.Added;
            _context.SaveChanges();
            return pet;
        }

        public Pet UpdatePet(Pet pet)
        {
            if (pet.PreviousOwner != null &&
                _context.ChangeTracker.Entries<Owner>()
                    .FirstOrDefault(oe => oe.Entity.Id == pet.PreviousOwner.Id) == null)
            {
                _context.Attach(pet.PreviousOwner);
            }
            else
            {
                _context.Entry(pet).Reference(p => p.PreviousOwner).IsModified = true;
            }

            var updated = _context.Update(pet).Entity;
            _context.SaveChanges();
            return updated;
        }

        public Pet RemovePet(int id)
        {
            var entityRemoved = _context.Remove(new Pet{Id = id}).Entity;
            _context.SaveChanges();
            return entityRemoved;
        }

        public Pet GetSinglePetById(int id)
        {
            return _context.Pets.FirstOrDefault(pet => pet.Id == id);
        }

        public Pet GetSinglePetByIdWithOwners(int id)
        {
            return _context.Pets.Include(p => p.PreviousOwner).FirstOrDefault(p => p.Id == id);
        }
    }
}