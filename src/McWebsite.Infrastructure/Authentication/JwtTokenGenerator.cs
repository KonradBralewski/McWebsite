using McWebsite.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Authentication
{
    internal sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(Guid id, string email, string password)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key")),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
            };

            var securityToken = new JwtSecurityToken(
                issuer: "McWebsite",
                audience : "McWebsite",
                expires : DateTime.Now.AddDays(1),
                claims: claims, 
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
