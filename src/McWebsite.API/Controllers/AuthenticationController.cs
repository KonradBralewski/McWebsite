using ErrorOr;
using McWebsite.API.Contracts.Auth;
using McWebsite.Application.Authentication.Commands;
using McWebsite.Application.Authentication.Commands.Register;
using McWebsite.Application.Authentication.Queries.Login;
using MediatR;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;

namespace McWebsite.API.Controllers
{
    [Route("auth")]
    public class AuthenticationController : McWebsiteController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

            return registerResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")] 
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);

            return loginResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }
    }
}
