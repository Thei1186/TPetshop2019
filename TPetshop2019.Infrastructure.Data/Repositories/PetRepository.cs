using System.Collections.Generic;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Infrastructure.Data.Repositories
{
    public class PetRepository: IPetRepository
    {
        public IEnumerable<Pet> ReadPets()
        {
            return FakeDB.PetTable;
        }
    }
}