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
            //context.Database.EnsureDeleted();
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
                Type = "Dog",
                SoldDate = new DateTime(2018, 8, 27)
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
                SoldDate = new DateTime(2006, 8, 27)
            };

            Pet p4 = new Pet
            {
                Birthdate = new DateTime(2018, 2, 25),
                Name = "Emergency Bacon Supply",
                PreviousOwner = peter,
                Price = 787,
                Type = "Pig",
                SoldDate = new DateTime(2019, 1, 27)
            };

            Pet p5 = new Pet
            {
                Birthdate = new DateTime(2004, 8, 5),
                Name = "MissHoots",
                PreviousOwner = lars,
                Price = 157.35,
                Type = "Owl",
                SoldDate = new DateTime(2005, 8, 21)
            };

            Pet p6 = new Pet
            {
                Birthdate = new DateTime(2016, 1, 5),
                Name = "McBleatsALot",
                PreviousOwner = lars,
                Price = 451.35,
                Type = "Goat",
                SoldDate = new DateTime(2018, 8, 27)
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

            PetColour pc3 = new PetColour()
            {
                Colour = gray,
                ColourId = gray.ColourId,
                Pet = p3,
                PetId = p3.PetId
            };

            PetColour pc4 = new PetColour()
            {
                Colour = white,
                ColourId = white.ColourId,
                Pet = p4,
                PetId = p4.PetId
            };

            PetColour pc5 = new PetColour()
            {
                Colour = gray,
                ColourId = gray.ColourId,
                Pet = p5,
                PetId = p5.PetId
            };

            PetColour pc6 = new PetColour()
            {
                Colour = black,
                ColourId = black.ColourId,
                Pet = p6,
                PetId = p6.PetId
            };
            #endregion

            #region Setup

            //p1.Colours.Add(pc1);
            //black.PetList.Add(pc1);


            context.PetColour.AddRange(pc1, pc2, pc3, pc4, pc5, pc6);
            context.Colours.AddRange(black, brown, gray, white);
            context.Owner.AddRange(peter, lars);
            context.Pets.AddRange(p1, p2, p3, p4, p5, p6);

            context.SaveChanges();
            #endregion
        }
    }
}