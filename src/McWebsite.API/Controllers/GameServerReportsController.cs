using MapsterMapper;
using McWebsite.Application.GameServers.Queries.GetGameServer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace McWebsite.API.Controllers
{
    public class GameServerReportsController : McWebsiteController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GameServerReportsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{gameServerId}")]
        public async Task<IActionResult> GetGameServerReportById([FromRoute] Guid gameServerReportId)
        {
            var query = _mapper.Map<GetGameServerQuery>(gameServerReportId);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                serverResult => Ok(_mapper.Map<GetGameServerResponse>(serverResult)),
                errors => Problem(errors));

        }

        [HttpGet]
        public async Task<IActionResult> GetGameServerReportsListAsync([FromQuery] int page, [FromQuery] int entriesPerPage)
        {
            var query = _mapper.Map<GetGameServersQuery>((page, entriesPerPage));

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                serversResult => Ok(_mapper.Map<GetGameServersResponse>(serversResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameServerReportAsync([FromBody] CreateGameServerRequest request)
        {
            var command = _mapper.Map<CreateGameServerCommand>(request);

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                serverResult => Created($"api/GameServers/{serverResult.Id}", _mapper.Map<CreateGameServerResponse>(serverResult)),
                errors => Problem(errors));

        }

        [HttpDelete("{gameServerId}")]
        public async Task<IActionResult> DeleteGameServerReportAsync([FromRoute] Guid gameServerId)
        {
            var command = _mapper.Map<DeleteGameServerCommand>(gameServerId);

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                result => NoContent(),
                errors => Problem(errors));

        }

        [HttpPatch("{gameServerId}")]
        public async Task<IActionResult> UpdateGameServerReportAsync([FromRoute] Guid gameServerId, [FromBody] UpdateGameServerRequest request)
        {
            var command = _mapper.Map<UpdateGameServerCommand>((gameServerId, request));

            var commandResult = await _mediator.Send(command);

            if (commandResult is null)
            {
                return NoContent();
            }

            return commandResult.Value.Match(
                result => Ok(_mapper.Map<UpdateGameServerResponse>(result)),
                errors => Problem(errors));

        }

    }
}
