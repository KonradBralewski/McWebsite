using ErrorOr;
using McWebsite.Application.Common.Interfaces.Authentication;
using MediatR;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.User;
using McWebsite.Domain.User.ValueObjects;
using Microsoft.AspNetCore.Identity;
using McWebsite.Application.Common.Interfaces.Services;

namespace McWebsite.Application.Authentication.Commands.Register
{
    internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAuthenticationService _authenticationService;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IAuthenticationService authenticationService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _authenticationService = authenticationService;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _authenticationService.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // Create user (generate unique Id)

            var user = User.Create(null, command.Email, command.Password, DateTime.UtcNow, DateTime.UtcNow);

            var addUserResult = await _authenticationService.AddUser(user);

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
