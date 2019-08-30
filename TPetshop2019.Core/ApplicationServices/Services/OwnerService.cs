using System.Collections.Generic;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices.Services
{
    public class OwnerService: IOwnerService
    {
        private IOwnerRepository _ownerRepo;
        public OwnerService(IOwnerRepository ownerRepo)
        {
            this._ownerRepo = ownerRepo;
        }

        public Owner CreateOwner(string firstName, string lastName, string address, string phoneNr, string email)
        {
            throw new System.NotImplementedException();
        }

        public Owner ReadOwner(Owner owner)
        {
            throw new System.NotImplementedException();
        }

        public Owner UpdateOwner(int id, string firstName, string lastName, string address, string phoneNr, string email)
        {
            throw new System.NotImplementedException();
        }

        public Owner DeleteOwner(Owner owner)
        {
            throw new System.NotImplementedException();
        }

        public List<Owner> ReadAllOwners()
        {
            throw new System.NotImplementedException();
        }
    }
}