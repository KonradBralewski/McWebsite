using McWebsite.Application.Services.Authentication;
using McWebsite.Shared.Contracts.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McWebsite.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var result = _authenticationService.Register(request.Email, request.Password);
            var response = new AuthenticationResponse(result.user.Id, result.user.Email, result.Token);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var result = _authenticationService.Login(request.Email, request.Password);
            var response = new AuthenticationResponse(result.user.Id, result.user.Email, result.Token);

            return Ok(response);
        }
    }
}
