﻿using System;
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
            var owner = ReadOwner(id);
            owner.Pets = _petRepo.ReadPets().Where(pet => pet.PreviousOwner.Id == owner.Id).ToList();
            return owner;
        }

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
    }
}