using ErrorOr;
using McWebsite.Application.Common.Interfaces.Authentication;
using McWebsite.Domain.User;
using MediatR;
using McWebsite.Domain.Common.Errors;
using McWebsite.Application.Common.Services;

namespace McWebsite.Application.Authentication.Queries.Login
{
    internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAuthenticationService _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IAuthenticationService userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Validate if user exists

            if (await _userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // Validate if password is correct

            if (user.Password != query.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
  
}
