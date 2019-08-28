using System;
using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Infrastructure.Data
{
    public class FakeDB
    {
        internal static int PetId = 1;
        internal static int OwnerId = 1;
        internal static IEnumerable<Pet> PetTable;
        internal static IEnumerable<Owner> OwnerTable;

        public static void InitData()
        {

            //Make a few owners above the pets and then use the owners as previous owner appropriately

            Pet p1 = new Pet
            {
                Id = PetId++,
                Birthdate = new DateTime(2017,2,10),
                Colour = "Dark Brown",
                Name = "Peter Barker",
                PreviousOwner = new Owner
                {
                    Id = OwnerId++,
                    FirstName = "Lars",
                    LastName = "Larsen"
                },
                Price = 350,
                Type = "Corgi"
            };
            
            Pet p2 = new Pet
            {
                Id = PetId++,
                Birthdate = new DateTime(2014, 4, 20),
                Colour = "light Brown",
                Name = "Magmadar",
                Price = 750.95,
                Type = "Poodle",
                SoldDate = new DateTime(2019,8,27)
            };

            PetTable = new List<Pet>{p1, p2};
            OwnerTable = new List<Owner>();
        }
    }
}