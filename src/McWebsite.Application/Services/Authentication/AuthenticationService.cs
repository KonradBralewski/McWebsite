using McWebsite.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Services.Authentication
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(Guid.NewGuid(), email, password);
        }

        public AuthenticationResult Register(string email, string password)
        {
            // Check if user already exists

            // Create user (generate unique Id)

            // Create JWT Token

            Guid userId = Guid.NewGuid();

            var token = _jwtTokenGenerator.GenerateToken(userId, email, password);

            return new AuthenticationResult(Guid.NewGuid(), email, token);
        }
    }
}
