using System.Collections.Generic;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Infrastructure.Data.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        public IEnumerable<Owner> GetOwners()
        {
            return FakeDB.OwnerTable;
        }
    }
}