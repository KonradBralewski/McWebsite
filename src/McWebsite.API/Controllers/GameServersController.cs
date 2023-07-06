using MapsterMapper;
using McWebsite.API.Contracts.GameServer;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using McWebsite.Application.GameServers.Queries.GetGameServer;

namespace McWebsite.API.Controllers
{
    [Route("api/[controller]")]
    public class GameServersController : McWebsiteController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GameServersController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{gameServerId}")]
        public async Task<IActionResult> GetGameServerById([FromRoute] Guid gameServerId)
        {
            var query = _mapper.Map<GetGameServerQuery>(gameServerId);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                serverResult => Ok(_mapper.Map<GetGameServerResponse>(serverResult)),
                errors => Problem(errors));

        }

        [HttpGet] 
        public async Task<IActionResult> GetGameServersListAsync([FromQuery] int page, [FromQuery] int entriesPerPage)
        {
            var query = _mapper.Map<GetGameServersQuery>((page, entriesPerPage));

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                serversResult => Ok(_mapper.Map<GetGameServersResponse>(serversResult)),
                errors => Problem(errors));
        }

    }
}
