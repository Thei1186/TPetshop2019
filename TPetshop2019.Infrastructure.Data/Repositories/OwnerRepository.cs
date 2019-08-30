﻿using System.Collections.Generic;
using System.Linq;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Infrastructure.Data.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        public IEnumerable<Owner> GetOwners()
        {
            return FakeDB.OwnerTable;
        }

        public Owner CreateOwner(Owner owner)
        {
            owner.Id = FakeDB.OwnerId++;
            var owners = FakeDB.OwnerTable.ToList();
            owners.Add(owner);
            FakeDB.OwnerTable = owners;
            return owner;
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            var owners = FakeDB.OwnerTable.ToList();
            var chosenOwner = owners.FirstOrDefault(owner => owner.Id == ownerToUpdate.Id);
            if (chosenOwner != null)
            {
                owners.Remove(chosenOwner);
                owners.Add(ownerToUpdate);
            }

            FakeDB.OwnerTable = owners;
            return ownerToUpdate;
        }

        public Owner DeleteOwner(Owner ownerToDelete)
        {
            var owners = FakeDB.OwnerTable.ToList();
            var chosenOwner = owners.FirstOrDefault(owner => owner.Id == ownerToDelete.Id);
            owners.Remove(chosenOwner);
            FakeDB.OwnerTable = owners;
            return chosenOwner;
        }
    }
}