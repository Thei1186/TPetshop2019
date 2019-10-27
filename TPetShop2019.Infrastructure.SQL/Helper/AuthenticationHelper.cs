using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.Infrastructure.SQL.Helper
{
    public class AuthenticationHelper: IAuthenticationHelper
    {

        private byte[] secretBytes;

        public AuthenticationHelper(Byte[] secret)
        {
            secretBytes = secret;
        }

        // This method generates and returns a JWT token for a user.
        public string GenerateToken(Owner owner)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, owner.Username)
            };

            if (owner.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(secretBytes),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null, // issuer - not needed (ValidateIssuer = false)
                    null, // audience - not needed (ValidateAudience = false)
                    claims.ToArray(),
                    DateTime.Now,               // notBefore
                    DateTime.Now.AddMinutes(10)));  // expires

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
