using MapsterMapper;
using McWebsite.API.Contracts.GameServerReport;
using McWebsite.API.Contracts;
using McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand;
using McWebsite.Application.GameServerReports.Commands.DeleteGameServerReportCommand;
using McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand;
using McWebsite.Application.GameServerReports.Queries.GetGameServerReportQuery;
using McWebsite.Application.GameServerReports.Queries.GetGameServersReportsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using McWebsite.Application.GameServerSubscriptions.Queries.GetGameServerSubscriptionQuery;
using McWebsite.Application.GameServerSubscriptions.Queries.GetGameServersSubscriptionsQuery;
using McWebsite.API.Controllers.Base;
using McWebsite.API.Contracts.GameServerSubscription;
using McWebsite.Application.GameServerSubscriptions.Commands.CreateGameServerSubscriptionCommand;
using McWebsite.Application.GameServerSubscriptions.Commands.DeleteGameServerSubscriptionCommand;
using McWebsite.Application.GameServerSubscriptions.Commands.UpdateGameServerSubscriptionCommand;

namespace McWebsite.API.Controllers
{
    public class GameServersSubscriptionsController : McWebsiteController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GameServersSubscriptionsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{gameServerSubscriptionId}")]
        public async Task<IActionResult> GetGameServerSubscriptionById([FromRoute] Guid gameServerSubscriptionId)
        {
            var query = _mapper.Map<GetGameServerSubscriptionQuery>(gameServerSubscriptionId);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                subscriptionResult => Ok(_mapper.Map<GetGameServerSubscriptionResponse>(subscriptionResult.GameServerSubscription)),
                errors => Problem(errors));

        }

        [HttpGet]
        public async Task<IActionResult> GetGameServersSubscriptionsListAsync([FromQuery] int page, [FromQuery] int entriesPerPage)
        {
            var query = _mapper.Map<GetGameServersSubscriptionsQuery>((page, entriesPerPage));

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                subscriptionsResult => Ok(_mapper.Map<GetGameServersSubscriptionsResponse>(subscriptionsResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameServerSubscriptionAsync([FromBody] CreateGameServerSubscriptionRequest request)
        {
            var command = _mapper.Map<CreateGameServerSubscriptionCommand>((this.RetrieveRequestSendingUserId(), request));

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                subscriptionResult => Created($"api/GameServersSubscriptions/{subscriptionResult.GameServerSubscription.Id.Value}",
                _mapper.Map<CreateGameServerSubscriptionResponse>(subscriptionResult)),
                errors => Problem(errors));

        }

        [HttpDelete("{gameServerSubscriptionId}")]
        public async Task<IActionResult> DeleteGameServerSubscriptionAsync([FromRoute] Guid gameServerSubscriptionId)
        {
            var command = _mapper.Map<DeleteGameServerSubscriptionCommand>(gameServerSubscriptionId);

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                result => NoContent(),
                errors => Problem(errors));

        }

        [HttpPatch("{gameServerSubscriptionId}")]
        public async Task<IActionResult> UpdateGameServerSubscriptionAsync([FromRoute] Guid gameServerSubscriptionId,
                                                                           [FromBody] UpdateGameServerSubscriptionRequest request)
        {
            var command = _mapper.Map<UpdateGameServerSubscriptionCommand>((gameServerSubscriptionId, request));

            var commandResult = await _mediator.Send(command);

            if (commandResult is null)
            {
                return NoContent();
            }

            return commandResult.Value.Match(
                result => Ok(_mapper.Map<UpdateGameServerSubscriptionResponse>(result)),
                errors => Problem(errors));

        }
    }
}
