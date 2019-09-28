using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices.Services
{
    public class OwnerService: IOwnerService
    {
        private readonly IOwnerRepository _ownerRepo;
        private readonly IValidateIdService _validateIdService;
        private readonly IPetRepository _petRepo;

        public OwnerService(IOwnerRepository ownerRepo, IValidateIdService validateIdService,
            IPetRepository petRepo)
        {
            this._ownerRepo = ownerRepo;
            this._validateIdService = validateIdService;
            this._petRepo = petRepo;
        }

        //old method
        public Owner NewOwner(string firstName, string lastName, string address, string phoneNr, string email)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new InvalidDataException("The owner needs both a first and last name");
            }

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

        public Owner ReadOwnerIncludePets(int id)
        {
            return _ownerRepo.GetOwnerByIdIncludePets(id);
        }

        //new method for restApi
        public Owner CreateOwner(Owner owner)
        {
            if (owner == null)
            {
                throw new InvalidDataException("The owner object is null and therefor invalid");
            }
            return _ownerRepo.CreateOwner(owner);
        }

        public Owner ReadOwner(int id)
        {
            _validateIdService.ValidateId(id);
            var ownerToGet = _ownerRepo.GetOwnerById(id);
            if (ownerToGet == null)
            {
                throw new InvalidDataException($"No owner with the id: {id} found");
            }

            return ownerToGet;
        }

        //old method
        public Owner UpdateOwner(int id, string firstName, string lastName, string address, string phoneNr, string email)
        {
            Owner ownerToUpdate = ReadOwner(id);
           
            ownerToUpdate.FirstName = firstName;
            ownerToUpdate.LastName = lastName;
            ownerToUpdate.Email = email;
            ownerToUpdate.Address = address;
            ownerToUpdate.PhoneNumber = phoneNr;
            return ownerToUpdate;
        }

        //new method for restApi
        public Owner MakeUpdatedOwner(Owner ownerToUpdate)
        {
            if (ownerToUpdate == null)
            {
                throw new InvalidDataException("Something went wrong with updating the owner");
            }
            return _ownerRepo.UpdateOwner(ownerToUpdate);
        }

        public Owner DeleteOwner(Owner owner)
        {
            if (owner == null)
            {
                throw new InvalidDataException($"Something went wrong and the owner was null");
            }
            return _ownerRepo.DeleteOwner(owner);
        }

        public List<Owner> ReadAllOwners()
        {
            return _ownerRepo.GetOwners().ToList();
        }

        public List<Owner> GetFilteredOwners(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                throw new InvalidDataException("CurrentPage and ItemsPage Must be zero or more");
            }
            if ((filter.CurrentPage - 1 * filter.ItemsPrPage) >= _ownerRepo.Count())
            {
                throw new InvalidDataException("Index out of bounds, CurrentPage is too high");
            }
            return _ownerRepo.GetOwners(filter).ToList();
        }
    }
}