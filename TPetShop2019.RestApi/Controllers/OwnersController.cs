using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPetshop2019.Core.ApplicationServices;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET api/owners
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get([FromQuery] Filter filter)
        {
            try
            {
                var filteredList = _ownerService.GetFilteredOwners(filter);
                return Ok(filteredList);

                //var newOwnerList = new List<object>();
                //foreach (var owner in filteredList)
                //{
                //    newOwnerList.Add(new
                //    {
                //        owner.Id,
                //        owner.FirstName,
                //        owner.LastName,
                //        owner.Email,
                //        owner.PhoneNumber,
                //    });
                //}

                //return Ok(newOwnerList);

                //return _ownerService.ReadAllOwners();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/owners
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            try
            {
                return _ownerService.ReadOwnerIncludePets(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/owners
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Owner> Post([FromBody]Owner owner)
        {
            try
            {
                 return _ownerService.CreateOwner(owner);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/owners
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            try
            {
             var ownerToDelete = _ownerService.ReadOwner(id);
             return Ok(_ownerService.DeleteOwner(ownerToDelete));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/owners
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            try
            {
                if (id <= 0 || id != owner.Id)
                {
                    return BadRequest("Parameter PetId and owner ID must be the same");
                }

                return Ok(_ownerService.MakeUpdatedOwner(owner));
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}