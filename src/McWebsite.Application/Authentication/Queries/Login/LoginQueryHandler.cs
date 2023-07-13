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
        private readonly IAuthenticationService _authenticationService;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IAuthenticationService authenticationService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _authenticationService = authenticationService;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Validate if user exists

            if (await _authenticationService.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // Validate if password is correct

            var credentialsMatchResult = await _authenticationService.DoCredentialsMatch(query.Email, query.Password);

            if (credentialsMatchResult.IsError)
            {
                return credentialsMatchResult.Errors;
            }

            // Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
  
}
