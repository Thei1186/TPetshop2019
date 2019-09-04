using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPetshop2019.Core.ApplicationServices;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        // GET api/pets
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            try
            {
                return _petService.GetPets();
            }
            catch (Exception e)
            {
                BadRequest(e);
                throw;
            }
        }

        // GET api/pets/1
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            try
            {
             if (id <= 0)
             {
                return BadRequest("Id must be greater than 0");
             }
             return _petService.ReadPet(id);
            }
            catch (Exception e)
            {
                BadRequest(e);
                throw;
            }
        }

        // POST api/pets
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            try
            {
              if (string.IsNullOrEmpty(pet.Name))
              {
                 return BadRequest("The pet needs a name");
              }
              return _petService.CreatePet(pet);
            }
            catch (Exception e)
            {
                BadRequest(e);
                throw;
            }
        }

        // DELETE api/pets
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            try
            {
                var petToDelete = _petService.ReadPet(id);
                return _petService.DeletePet(petToDelete);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        // PUT api/pets
        [HttpPut("{id}")]
        public ActionResult<Pet> Update(int id, [FromBody] Pet pet)
        {
            try
            {
                if (id <= 0 || id != pet.Id)
                {
                    return BadRequest("Parameter Id and pet ID must be the same");
                }

                return Ok(_petService.MakeUpdatedPet(pet));

            }
            catch (Exception e)
            {
                BadRequest(e.Message);
                throw;
            }
        }
        
    }
}