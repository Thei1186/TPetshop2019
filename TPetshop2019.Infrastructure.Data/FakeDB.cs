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
            Owner peter = new Owner
            {
                Id = OwnerId++,
                FirstName = "Peter",
                LastName = "Petersen",
                Address = "Petersengade 10",
                Email = "Peter@petermail.com",
                PhoneNumber = "75202020"
            };

            Owner lars = new Owner
            {
                Id = OwnerId++,
                FirstName = "Lars",
                LastName = "Larsen",
                Address = "Larsgade 10",
                Email = "Lars@petermail.com",
                PhoneNumber = "75304020"
            };

            Pet p1 = new Pet
            {
                Id = PetId++,
                Birthdate = new DateTime(2017,2,10),
                Colour = "Dark Brown",
                Name = "Peter Barker",
                PreviousOwner = peter,
                Price = 350,
                Type = "Corgi"
            };
            
            Pet p2 = new Pet
            {
                Id = PetId++,
                Birthdate = new DateTime(2014, 4, 20),
                Colour = "light Brown",
                Name = "Magmadar",
                PreviousOwner = lars,
                Price = 750.95,
                Type = "Poodle",
                SoldDate = new DateTime(2019,8,27)
            };

            PetTable = new List<Pet>{p1, p2};
            OwnerTable = new List<Owner>{peter, lars};
        }
    }
}