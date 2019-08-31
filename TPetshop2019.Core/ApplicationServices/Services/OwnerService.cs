using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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
            Owner owner = new Owner
            {
                Address = address,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNr
            };
            return _ownerRepo.CreateOwner(owner);
        }

        public Owner ReadOwner(int id)
        {
            return _ownerRepo.GetOwners().ToList().FirstOrDefault(owner => owner.Id == id);
        }

        public Owner UpdateOwner(int id, string firstName, string lastName, string address, string phoneNr, string email)
        {
            Owner ownerToUpdate = ReadOwner(id);

            if (ownerToUpdate != null)
            {
                ownerToUpdate.FirstName = firstName;
                ownerToUpdate.LastName = lastName;
                ownerToUpdate.Email = email;
                ownerToUpdate.Address = address;
                ownerToUpdate.PhoneNumber = phoneNr;
            }
            return ownerToUpdate;
        }

        public Owner DeleteOwner(Owner owner)
        {
            return _ownerRepo.DeleteOwner(owner);
        }

        public List<Owner> ReadAllOwners()
        {
            return _ownerRepo.GetOwners().ToList();
        }
    }
}