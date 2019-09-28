namespace TPetshop2019.Core.Entity
{
    public class PetColour
    {
        public int ColourId { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public Colour Colour { get; set; }
    }
}