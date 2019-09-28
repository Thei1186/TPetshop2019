using System;
using System.Collections.Generic;

namespace TPetshop2019.Core.Entity
{
    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime SoldDate { get; set; }
        public double Price { get; set; }
        public List<PetColour> Colours { get; set; }
        public Owner PreviousOwner { get; set; }

    }

}