using ErrorOr;
using McWebsite.Application.Common.Interfaces.Authentication;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.User;
using McWebsite.Domain.User.ValueObjects;

namespace McWebsite.Application.Authentication.Commands.Register
{
    internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // Create user (generate unique Id)

            var user = User.Create(null, command.Email, command.Password);

            _userRepository.AddUser(user);

            // Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
