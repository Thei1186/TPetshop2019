using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.DomainServices
{
    public interface IPetRepository
    {
        /// <summary>
        /// Gets all pets from the database
        /// </summary>
        /// <returns>An IEnumerable list of pets</returns>
        IEnumerable<Pet> ReadPets();
        
        /// <summary>
        /// Adds a pet object to the list in the database
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        Pet AddPet(Pet pet);

        /// <summary>
        /// Updates a pet
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>The updated pet</returns>
        Pet UpdatePet(Pet pet);

        /// <summary>
        /// Removes pet from the database based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The removed pet</returns>
        Pet RemovePet(int id);

        /// <summary>
        /// Gets a pet by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The pet with the given id</returns>
        Pet GetSinglePetById(int id);
    }
}