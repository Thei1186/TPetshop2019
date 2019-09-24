using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices
{
    public interface IOwnerService
    {
        /// <summary>
        /// Creates a new owner based on user input.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="phoneNr"></param>
        /// <param name="email"></param>
        /// <returns>The new owner</returns>
        Owner NewOwner(string firstName, string lastName, string address, string phoneNr, string email);

        /// <summary>
        /// Gets a owner and also their former pets
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Owner ReadOwnerIncludePets(int id);
        /// <summary>
        /// Creates a new owner in the database based on an owner object. Atm. used by the RestApi
        /// </summary>
        /// <param name="owner"></param>
        /// <returns>The new owner</returns>
        Owner CreateOwner(Owner owner);
        
        /// <summary>
        /// Finds a user based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The matching owner</returns>
        Owner ReadOwner(int id);

        /// <summary>
        /// Updates an owner based on user input.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="phoneNr"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Owner UpdateOwner(int id, string firstName, string lastName, string address, string phoneNr, string email);

        /// <summary>
        /// Deletes an owner
        /// </summary>
        /// <param name="owner"></param>
        /// <returns>The deleted owner</returns>
        Owner DeleteOwner(Owner owner);

        /// <summary>
        /// Gets all the current owners from the database
        /// </summary>
        /// <returns>A list of owners</returns>
        List<Owner> ReadAllOwners();

        /// <summary>
        /// Returns a filtered list of owners
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<Owner> GetFilteredOwners(Filter filter);

        /// <summary>
        /// Updates an owner. This method is used in the RestAPI
        /// </summary>
        /// <param name="ownerToUpdate"></param>
        /// <returns>The updated owner</returns>
        Owner MakeUpdatedOwner(Owner ownerToUpdate);
    }
}