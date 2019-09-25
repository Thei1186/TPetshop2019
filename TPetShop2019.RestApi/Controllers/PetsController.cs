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
        public ActionResult<IEnumerable<Pet>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_petService.GetFilteredPets(filter));
               // return Ok(_petService.GetPets());
            }
            catch (Exception e)
            {
               return BadRequest(e);
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
                return BadRequest("PetId must be greater than 0");
             }
             return _petService.ReadPetWithOwners(id);
            }
            catch (Exception e)
            {
               return BadRequest(e);
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
               return BadRequest(e);
            }
        }

        // DELETE api/pets
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            try
            {
                return Ok(_petService.DeletePet(id > 0 ? new Pet() {PetId = id} : null));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/pets
        [HttpPut("{id}")]
        public ActionResult<Pet> Update(int id, [FromBody] Pet pet)
        {
            try
            {
                if (id <= 0 || id != pet.PetId)
                {
                    return BadRequest("Parameter PetId and pet ID must be the same");
                }

                return Ok(_petService.MakeUpdatedPet(pet));

            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
        
    }
}