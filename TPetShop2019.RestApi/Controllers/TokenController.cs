using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private IOwnerRepository _repo;
        private IAuthenticationHelper _authService;

        public TokenController(IOwnerRepository repo, IAuthenticationHelper authService)
        {
            _repo = repo;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginInputModel model)
        {
            var user = _repo.GetOwners().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!_authService.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = _authService.GenerateToken(user)
            });
        }
    }
}