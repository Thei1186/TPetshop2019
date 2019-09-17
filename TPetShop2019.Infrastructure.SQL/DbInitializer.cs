using System;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.Infrastructure.SQL
{
    public class DbInitializer
    {
        public static void SeedDb(PetShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Owner peter = new Owner
            {
                FirstName = "Peter",
                LastName = "Petersen",
                Address = "Petersengade 10",
                Email = "Peter@petermail.com",
                PhoneNumber = "75202020"
            };

            Owner lars = new Owner
            {
                FirstName = "Lars",
                LastName = "Larsen",
                Address = "Larsgade 10",
                Email = "Lars@petermail.com",
                PhoneNumber = "75304020"
            };
            Pet p1 = new Pet
            {
                
                Birthdate = new DateTime(2017, 2, 10),
                Colour = "Dark Brown",
                Name = "Peter Barker",
                PreviousOwner = peter,
                Price = 350,
                Type = "Dog"
            };

            Pet p2 = new Pet
            {
                
                Birthdate = new DateTime(2014, 4, 20),
                Colour = "Dark Red",
                Name = "Magmadar",
                PreviousOwner = lars,
                Price = 750.95,
                Type = "Dog",
                SoldDate = new DateTime(2019, 8, 27)
            };

            Pet p3 = new Pet
            {
                Birthdate = new DateTime(2004, 8, 5),
                Colour = "Dark Brown",
                Name = "Ser Slithers",
                PreviousOwner = lars,
                Price = 557.35,
                Type = "Snake",
            };

            Pet p4 = new Pet
            {
                Birthdate = new DateTime(2018, 2, 25),
                Colour = "Pink",
                Name = "Emergency Bacon Supply",
                PreviousOwner = peter,
                Price = 787,
                Type = "Pig",
            };

            Pet p5 = new Pet
            {
                Birthdate = new DateTime(2004, 8, 5),
                Colour = "White",
                Name = "MissHoots",
                PreviousOwner = lars,
                Price = 157.35,
                Type = "Owl"
            };

            Pet p6 = new Pet
            {
                Birthdate = new DateTime(2016, 1, 5),
                Colour = "Tan",
                Name = "McBleatsALot",
                PreviousOwner = lars,
                Price = 451.35,
                Type = "Goat"
            };

            context.Pets.AddRange(p1,p2,p3,p4,p5,p6);
            context.Owner.AddRange(peter,lars);
            context.SaveChanges();
        }
    }
}