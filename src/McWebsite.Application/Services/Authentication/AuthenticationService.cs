using McWebsite.Application.Common.Interfaces.Authentication;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Entities;
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
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            // Validate if user exists

            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email address does not exists.");
            }

            // Validate if password is correct

            if(user.Password != password)
            {
                throw new Exception("Given password is invalid.");
            }

            // Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public AuthenticationResult Register(string email, string password)
        {
            // Check if user already exists

            if(_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given email address already exists.");
            }

            // Create user (generate unique Id)

            var user = new User
            {
                Email = email,
                Password = password
            };

            _userRepository.AddUser(user);

            // Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
