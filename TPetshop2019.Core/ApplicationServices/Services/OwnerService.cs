using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public Owner NewOwner(string firstName, string lastName, string address, string phoneNr, string email)
        {
            Owner owner = new Owner
            {
                Address = address,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNr
            };
            return CreateOwner(owner);
        }

        public Owner CreateOwner(Owner owner)
        {
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
        public Owner MakeUpdatedOwner(Owner ownerToUpdate)
        {
            var owner = ReadOwner(ownerToUpdate.Id);
            owner.FirstName = ownerToUpdate.FirstName;
            owner.LastName = ownerToUpdate.LastName;
            owner.Address = ownerToUpdate.Address;
            owner.Email = ownerToUpdate.Email;
            owner.PhoneNumber = ownerToUpdate.PhoneNumber;
            return _ownerRepo.UpdateOwner(owner);
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