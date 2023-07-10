using MapsterMapper;
using McWebsite.API.Contracts.GameServer;
using McWebsite.API.Contracts.GameServerReport;
using McWebsite.API.Controllers.Base;
using McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand;
using McWebsite.Application.GameServerReports.Queries.GetGameServersReportsQuery;
using McWebsite.Application.GameServers.Queries.GetGameServer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace McWebsite.API.Controllers
{
    public class GameServersReportsController : McWebsiteController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GameServersReportsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{gameServerReportId}")]
        public async Task<IActionResult> GetGameServerReportById([FromRoute] Guid gameServerReportId)
        {
            var query = _mapper.Map<GetGameServerQuery>(gameServerReportId);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                serverResult => Ok(_mapper.Map<GetGameServerReportResponse>(serverResult)),
                errors => Problem(errors));

        }

        [HttpGet]
        public async Task<IActionResult> GetGameServersReportsListAsync([FromQuery] int page, [FromQuery] int entriesPerPage)
        {
            var query = _mapper.Map<GetGameServersReportsQuery>((page, entriesPerPage));

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                serversResult => Ok(_mapper.Map<GetGameServersResponse>(serversResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameServerReportAsync([FromBody] CreateGameServerReportRequest request)
        {
            var command = _mapper.Map<CreateGameServerReportCommand>((this.RetrieveRequestSendingUserId(), request));

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                serverReportResult => Created($"api/GameServersReports/{serverReportResult.GameServerReport.Id.Value}",
                _mapper.Map<CreateGameServerReportResponse>(serverReportResult)),
                errors => Problem(errors));

        }

        //[HttpDelete("{gameServerId}")]
        //public async Task<IActionResult> DeleteGameServerReportAsync([FromRoute] Guid gameServerId)
        //{
        //    var command = _mapper.Map<DeleteGameServerCommand>(gameServerId);

        //    var commandResult = await _mediator.Send(command);

        //    return commandResult.Match(
        //        result => NoContent(),
        //        errors => Problem(errors));

        //}

        //[HttpPatch("{gameServerId}")]
        //public async Task<IActionResult> UpdateGameServerReportAsync([FromRoute] Guid gameServerId, [FromBody] UpdateGameServerReportRequest request)
        //{
        //    var command = _mapper.Map<UpdateGameServerCommand>((gameServerId, request));

        //    var commandResult = await _mediator.Send(command);

        //    if (commandResult is null)
        //    {
        //        return NoContent();
        //    }

        //    return commandResult.Value.Match(
        //        result => Ok(_mapper.Map<UpdateGameServerResponse>(result)),
        //        errors => Problem(errors));

        //}

    }
}
