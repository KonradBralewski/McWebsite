using MapsterMapper;
using McWebsite.API.Contracts.GameServer;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameServersController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GameServersController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet] public async Task<IActionResult> GetGameServersListAsync([FromQuery] int page, [FromQuery] int entriesPerPage)
        {
            var query = _mapper.Map<GetGameServersQuery>((page, entriesPerPage));

            var queryResult = await _mediator.Send(query);

            var result = _mapper.Map<GetGameServersResponse>(queryResult);

            return Ok(result);
        }
    }
}
