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
            _context.Remove(owner);
            _context.SaveChanges();
            return owner;
        }

        public Owner GetOwnerById(int id)
        {
            return _context.Owner.FirstOrDefault(o => o.Id == id);
        }
    }
}