using System.Collections.Generic;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices.Services
{
    public class PetService : IPetService
    {
        private IPetRepository _petRepo;

        public PetService(IPetRepository petRepo)
        {
            this._petRepo = petRepo;
        }

        public IEnumerable<Pet> GetPets()
        {
            return this._petRepo.ReadPets();
        }
    }

}