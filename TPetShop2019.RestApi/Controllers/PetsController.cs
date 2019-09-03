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
                Console.WriteLine(e);
                throw;
            }
        }
        
        // POST api/pets
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            if (string.IsNullOrEmpty(pet.Name))
            {
                return BadRequest("The pet needs a name");
            }
            return _petService.CreatePet(pet);
        }
        
        // DELETE api/pets
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete([FromBody] int id)
        {
            var petToDelete = _petService.ReadPet(id);
            return _petService.DeletePet(petToDelete);
        }
        
        // PUT api/pets
        [HttpPut("{id}")]
        public ActionResult<Pet> Update(int id, [FromBody] Pet pet)
        {
            if (id <= 0 || id != pet.Id)
            {
                return BadRequest("Parameter Id and pet ID must be the same");
            }

            return _petService.MakeUpdatedPet(pet);
        }
        
    }
}