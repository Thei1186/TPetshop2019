using System;
using System.Collections.Generic;
using System.Linq;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.Infrastructure.SQL
{
    public class DbInitializer
    {
        public static void SeedDb(PetShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            #region Owner
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
            #endregion

            #region Colours
            var black = new Colour()
            {
                PetColour = "black"
            };

            var brown = new Colour()
            {
                PetColour = "brown"
            };

            var white = new Colour()
            {
                PetColour = "white"
            };

            var gray = new Colour()
            {
                PetColour = "gray"
            };


            #endregion

            #region pets
            Pet p1 = new Pet
            {
                
                Birthdate = new DateTime(2017, 2, 10),
                Name = "Peter Barker",
                PreviousOwner = peter,
                Colours = new List<PetColour>(),
                Price = 350,
                Type = "Dog"
            };

            Pet p2 = new Pet
            {
                
                Birthdate = new DateTime(2014, 4, 20),
                Name = "Magmadar",
                PreviousOwner = lars,
                Price = 750.95,
                Type = "Dog",
                SoldDate = new DateTime(2019, 8, 27)
            };

            Pet p3 = new Pet
            {
                Birthdate = new DateTime(2004, 8, 5),
                Name = "Ser Slithers",
                PreviousOwner = lars,
                Price = 557.35,
                Type = "Snake",
            };

            Pet p4 = new Pet
            {
                Birthdate = new DateTime(2018, 2, 25),
                Name = "Emergency Bacon Supply",
                PreviousOwner = peter,
                Price = 787,
                Type = "Pig",
            };

            Pet p5 = new Pet
            {
                Birthdate = new DateTime(2004, 8, 5),
                Name = "MissHoots",
                PreviousOwner = lars,
                Price = 157.35,
                Type = "Owl"
            };

            Pet p6 = new Pet
            {
                Birthdate = new DateTime(2016, 1, 5),
                Name = "McBleatsALot",
                PreviousOwner = lars,
                Price = 451.35,
                Type = "Goat"
            };

            #endregion

            #region Pet colour

            PetColour pc1 = new PetColour
            {
                Colour = black,
                ColourId = black.ColourId,
                Pet = p1,
                PetId = p1.PetId
            };

            PetColour pc2 = new PetColour
            {
                Colour = brown,
                ColourId = brown.ColourId,
                Pet = p2,
                PetId = p2.PetId
            };
            #endregion

            #region Setup

            context.Colours.Add(black);
            p1.Colours.Add(pc1);
            context.PetColour.Add(pc1);
            black.PetList.Add(pc1);
            #endregion
            context.Owner.AddRange(peter,lars);
            context.Pets.AddRange(p1,p2,p3,p4,p5,p6);
            context.SaveChanges();
        }
    }
}