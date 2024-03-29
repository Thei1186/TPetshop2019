﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.Infrastructure.SQL.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        private readonly PetShopContext _context;

        public OwnerRepository(PetShopContext context)
        {
            _context = context;
        }

        public FilteredList<Owner> GetOwners(Filter filter)
        {
            var filteredList = new FilteredList<Owner>();
            if (filter != null && filter.CurrentPage > 0 && filter.ItemsPrPage > 0 )
            {
                 filteredList.List = _context.Owner.Include(o => o.Pets).ThenInclude(p => p.Colours).Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage).ToList();
                 filteredList.Count = _context.Owner.Count();
                 return filteredList;
            }
            filteredList.List = _context.Owner.Include(o => o.Pets)
                .ThenInclude(p => p.Colours)
                .ToList();
            return filteredList;
        }

        public Owner CreateOwner(Owner owner)
        {
            var changeTracker = _context.ChangeTracker.Entries<Pet>();
            
            _context.Attach(owner).State = EntityState.Added;
            _context.SaveChanges();
            return owner;
        }

        public Owner UpdateOwner(Owner owner)
        {
            _context.Attach(owner).State = EntityState.Modified;
            _context.Entry(owner).Collection(o => o.Pets).IsModified = true;
            _context.SaveChanges();
            
            return owner;
        }

        public Owner DeleteOwner(Owner owner)
        {
            var ownerToRemove = _context.Remove(owner).Entity;
            _context.SaveChanges();
            return ownerToRemove;
        }

        public Owner GetOwnerById(int id)
        {
            return _context.Owner.FirstOrDefault(o => o.Id == id);
        }

        public Owner GetOwnerByIdIncludePets(int id)
        {
            return _context.Owner
                .Include(o => o.Pets)
                .ThenInclude(p => p.Colours)
                .ThenInclude(c => c.Colour)
                .FirstOrDefault(o => o.Id == id);
        }

        public int Count()
        {
            return _context.Owner.Count();
        }
    }
}