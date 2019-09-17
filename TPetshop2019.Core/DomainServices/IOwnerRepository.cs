using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.DomainServices
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetOwners();
        Owner CreateOwner(Owner owner);
        Owner UpdateOwner(Owner owner);
        Owner DeleteOwner(Owner owner);
        Owner GetOwnerById(int id);
        Owner GetOwnerByIdIncludePets(int id);
    }
}