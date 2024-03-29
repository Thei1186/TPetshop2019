﻿using System;
using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.ApplicationServices
{
    public interface IPetService
    {   
        /// <summary>
        /// Gets the list of current pets
        /// </summary>
        /// <returns> an IEnumerable list of pets</returns>
        FilteredList<Pet> GetPets();

        /// <summary>
        /// Creates a pet based on the specified parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="colour"></param>
        /// <param name="type"></param>
        /// <param name="price"></param>
        /// <param name="birthDate"></param>
        /// <returns>Returns the pet which were created</returns>
        Pet NewPet(string name, string colour, string type, double price, DateTime birthDate);

        /// <summary>
        /// Calls the repository's create pet method and parses in the pet it receives from the NewPet method'
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>The new pet</returns>
        Pet CreatePet(Pet pet);

        /// <summary>
        /// Accesses the pet with the matching PetId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the accessed pet</returns>
        Pet ReadPet(int id);

        /// <summary>
        /// Gets the pet with the matching PetId and includes
        /// owners
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Pet ReadPetWithOwners(int id);

        /// <summary>
        /// Updates a pet with the inserted parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="colour"></param>
        /// <param name="price"></param>
        /// <param name="birthdate"></param>
        /// <param name="soldDate"></param>
        /// <param name="previousOwner"></param>
        /// <returns>Returns the updated pet</returns>
        Pet UpdatePet(int id, string name, string type, string colour, double price,
            DateTime birthdate, DateTime soldDate, Owner previousOwner);

        /// <summary>
        /// Gets the updated pet and parses it to the repository
        /// </summary>
        /// <param name="petToUpdate"></param>
        /// <returns>The updated pet</returns>
        Pet MakeUpdatedPet(Pet petToUpdate);
        
        /// <summary>
        /// Deletes the pet
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>Returns the deleted pet</returns>
        Pet DeletePet(Pet pet);

        /// <summary>
        /// Sort pets 
        /// </summary>
        /// <returns>A list of pets</returns>
        List<Pet> sortPets();

        /// <summary>
        /// Gets the five cheapest pets or returns the list
        /// if length is under 5
        /// </summary>
        /// <returns></returns>
        List<Pet> GetFiveCheapestPets();

       /// <summary>
       /// Searches for pets that fit the query
       /// </summary>
       /// <param name="query"></param>
       /// <returns>A list of pets that match query</returns>
        List<Pet> SearchPets(string query);

       /// <summary>
        /// Adds a previous owner to the parsed pet
        /// </summary>
        /// <param name="newPet"></param>
        /// <param name="prevOwner"></param>
        /// <returns>The changed pet</returns>
        Pet AddOwnerToPet(Pet newPet, Owner prevOwner);
        
       /// <summary>
        /// Gets a filtered list of pets 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>a filtered list based on the requested filter</returns>
       FilteredList<Pet> GetFilteredPets(Filter filter);
    }
}