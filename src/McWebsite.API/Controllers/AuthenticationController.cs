using ErrorOr;
using McWebsite.API.Contracts.Auth;
using McWebsite.Application.Authentication.Commands;
using McWebsite.Application.Authentication.Commands.Register;
using McWebsite.Application.Authentication.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace McWebsite.API.Controllers
{
    [Route("auth")]
    public class AuthenticationController : McWebsiteController
    {
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var command = new RegisterCommand(request.Email, request.Password);
            ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

            return registerResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);

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
