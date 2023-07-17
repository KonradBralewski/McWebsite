using Mapster;
using MapsterMapper;
using McWebsite.API.Contracts.InGameEvent;
using McWebsite.API.Controllers.Base;
using McWebsite.Application.InGameEvents.Commands.CreateInGameEventCommand;
using McWebsite.Application.InGameEvents.Commands.DeleteInGameEventCommand;
using McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventQuery;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace McWebsite.API.Controllers
{
    public class InGameEventsController : McWebsiteController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public InGameEventsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{inGameEventId}")]
        public async Task<IActionResult> GetInGameEventById([FromRoute] Guid inGameEventId)
        {
            var query = _mapper.Map<GetInGameEventQuery>(inGameEventId);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                inGameEventResult => Ok(inGameEventResult.Adapt<GetInGameEventResponse>()),
                errors => Problem(errors));

        }

        [HttpGet]
        public async Task<IActionResult> GetInGameEventsListAsync([FromQuery] int page, [FromQuery] int entriesPerPage)
        {
            var query = _mapper.Map<GetInGameEventsQuery>((page, entriesPerPage));

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                inGameEventsResult => Ok(_mapper.Map<GetInGameEventsResponse>(inGameEventsResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateInGameEventAsync([FromBody] CreateInGameEventRequest request)
        {
            var command = _mapper.Map<CreateInGameEventCommand>(request);

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                inGameEventResult => Created($"api/InGameEvents/{inGameEventResult.InGameEvent.Id.Value}", _mapper.Map<CreateInGameEventResponse>(inGameEventResult)),
                errors => Problem(errors));

        }

        [HttpDelete("{inGameEventId}")]
        public async Task<IActionResult> DeleteInGameEventAsync([FromRoute] Guid inGameEventId)
        {
            var command = _mapper.Map<DeleteInGameEventCommand>(inGameEventId);

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                result => NoContent(),
                errors => Problem(errors));

        }

        [HttpPatch("{inGameEventId}")]
        public async Task<IActionResult> UpdateInGameEventAsync([FromRoute] Guid inGameEventId, [FromBody] UpdateInGameEventRequest request)
        {
            var command = _mapper.Map<UpdateInGameEventCommand>((inGameEventId, request));

            var commandResult = await _mediator.Send(command);

            if (commandResult is null)
            {
                return NoContent();
            }

            return commandResult.Value.Match(
                result => Ok(_mapper.Map<UpdateInGameEventResponse>(result)),
                errors => Problem(errors));

        }

    }
}
