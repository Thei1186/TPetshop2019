using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices
{
    public interface IOwnerService
    {
        Owner CreateOwner(string firstName, string lastName, string address, string phoneNr, string email);
        Owner ReadOwner(Owner owner);
        Owner UpdateOwner(int id, string firstName, string lastName, string address, string phoneNr, string email);
        Owner DeleteOwner(Owner owner);
        List<Owner> ReadAllOwners();
    }
}