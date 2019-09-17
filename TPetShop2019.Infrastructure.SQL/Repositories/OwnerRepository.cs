using System.Collections.Generic;
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

        public IEnumerable<Owner> GetOwners()
        {
           return _context.Owner.ToList();
        }

        public Owner CreateOwner(Owner owner)
        {
            _context.Attach(owner).State = EntityState.Added;
            _context.SaveChanges();
            return owner;
        }

        public Owner UpdateOwner(Owner owner)
        {
            throw new System.NotImplementedException();
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
            return _context.Owner.Include(o => o.Pets)
                .FirstOrDefault(o => o.Id == id);
        }
    }
}