using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices
{
    public interface IOwnerService
    {
        Owner NewOwner(string firstName, string lastName, string address, string phoneNr, string email);

        Owner CreateOwner(Owner owner);
        Owner ReadOwner(int id);
        Owner UpdateOwner(int id, string firstName, string lastName, string address, string phoneNr, string email);
        Owner DeleteOwner(Owner owner);
        List<Owner> ReadAllOwners();
        Owner MakeUpdatedOwner(Owner ownerToUpdate);
    }
}