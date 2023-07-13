using ErrorOr;
using McWebsite.Application.Common.Interfaces.Authentication;
using MediatR;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.User;
using McWebsite.Domain.User.ValueObjects;
using Microsoft.AspNetCore.Identity;
using McWebsite.Application.Common.Services;

namespace McWebsite.Application.Authentication.Commands.Register
{
    internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAuthenticationService _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IAuthenticationService userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // Create user (generate unique Id)

            var user = User.Create(null, command.Email, command.Password, DateTime.UtcNow, DateTime.UtcNow);

            var addUserResult = await _userRepository.AddUser(user);

            if (addUserResult.IsError)
            {
                return addUserResult.Errors;
            }

            // Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
