using ErrorOr;
using McWebsite.Application.Services.Authentication;
using McWebsite.Shared.Contracts.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace McWebsite.API.Controllers
{
    [Route("auth")]
    public class AuthenticationController : McWebsiteController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            ErrorOr<AuthenticationResult> registerResult = _authenticationService.Register(request.Email, request.Password);

            return registerResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            ErrorOr<AuthenticationResult> loginResult = _authenticationService.Login(request.Email, request.Password);

            return loginResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }


        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(authResult.User.Id, authResult.User.Email, authResult.Token);
        }
    }
}
