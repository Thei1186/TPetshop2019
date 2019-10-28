using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.DomainServices
{
    public interface IAuthenticationHelper
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        string GenerateToken(Owner owner);
    }
}