using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices
{
    public interface IPetService
    {
        IEnumerable<Pet> GetPets();


    }
}