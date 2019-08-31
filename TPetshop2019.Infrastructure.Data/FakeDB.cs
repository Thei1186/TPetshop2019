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
                Type = "Dog"
            };
            
            Pet p2 = new Pet
            {
                Id = PetId++,
                Birthdate = new DateTime(2014, 4, 20),
                Colour = "Dark Red",
                Name = "Magmadar",
                PreviousOwner = lars,
                Price = 750.95,
                Type = "Dog",
                SoldDate = new DateTime(2019,8,27)
            };

            Pet p3 = new Pet
            {
                Id = PetId++,
                Birthdate = new DateTime(2004, 8, 5),
                Colour = "Dark Brown",
                Name = "Ser Slithers",
                PreviousOwner = lars,
                Price = 557.35,
                Type = "Snake",
            };

            Pet p4 = new Pet
            {
                Id = PetId++,
                Birthdate = new DateTime(2018, 2, 25),
                Colour = "Pink",
                Name = "Emergency Bacon Supply",
                PreviousOwner = peter,
                Price = 787,
                Type = "Pig",
            };

            Pet p5 = new Pet
            {
                Id = PetId++,
                Birthdate = new DateTime(2004, 8, 5),
                Colour = "White",
                Name = "MissHoots",
                PreviousOwner = lars,
                Price = 157.35,
                Type = "Owl"
            };

            Pet p6 = new Pet
            {
                Id = PetId++,
                Birthdate = new DateTime(2016, 1, 5),
                Colour = "Tan",
                Name = "McBleatsALot",
                PreviousOwner = lars,
                Price = 451.35,
                Type = "Goat"
            };

            PetTable = new List<Pet>{p1, p2, p3, p4, p5, p6};
            OwnerTable = new List<Owner>{peter, lars};
        }
    }
}