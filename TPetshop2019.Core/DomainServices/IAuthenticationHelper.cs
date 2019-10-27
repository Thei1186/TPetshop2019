using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.DomainServices
{
    public interface IAuthenticationHelper
    {
        string GenerateToken(Owner owner);
    }
}