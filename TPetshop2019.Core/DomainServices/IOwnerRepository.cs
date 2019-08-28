using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.DomainServices
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetOwners();
    }
}