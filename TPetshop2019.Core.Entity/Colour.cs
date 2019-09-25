using System.Collections.Generic;

namespace TPetshop2019.Core.Entity
{
    public class Colour
    {
        public List<PetColour> PetList { get; set; }

        public int ColourId { get; set; }

        public string PetColour { get; set; }
    }
}