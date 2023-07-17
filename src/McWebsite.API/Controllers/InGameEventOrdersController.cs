using Mapster;
using MapsterMapper;
using McWebsite.API.Contracts.InGameEventOrder;
using McWebsite.API.Controllers.Base;
using McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand;
using McWebsite.Application.InGameEventOrders.Commands.DeleteInGameEventOrderCommand;
using McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand;
using McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrderQuery;
using McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrdersQuery;
using McWebsite.Application.InGameEvents.Commands.CreateInGameEventCommand;
using McWebsite.Application.InGameEvents.Commands.DeleteInGameEventCommand;
using McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventQuery;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace McWebsite.API.Controllers
{
    public class InGameEventOrdersController : McWebsiteController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public InGameEventOrdersController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{inGameEventOrderId}")]
        public async Task<IActionResult> GetInGameEventOrderById([FromRoute] Guid inGameEventOrderId)
        {
            var query = _mapper.Map<GetInGameEventOrderQuery>(inGameEventOrderId);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                inGameEventOrderResult => Ok(inGameEventOrderResult.InGameEventOrder.Adapt<GetInGameEventOrderResponse>()),
                errors => Problem(errors));

        }

        [HttpGet]
        public async Task<IActionResult> GetInGameEventOrdersListAsync([FromQuery] int page, [FromQuery] int entriesPerPage)
        {
            var query = _mapper.Map<GetInGameEventOrdersQuery>((page, entriesPerPage));

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                inGameEventOrdersResult => Ok(_mapper.Map<GetInGameEventOrdersResponse>(inGameEventOrdersResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateInGameEventOrderAsync([FromBody] CreateInGameEventOrderRequest request)
        {
            var command = _mapper.Map<CreateInGameEventOrderCommand>((this.RetrieveRequestSendingUserId(), request));

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                inGameEventResult => Created($"api/InGameEventOrders/{inGameEventResult.InGameEventOrder.Id.Value}", _mapper.Map<CreateInGameEventOrderResponse>(inGameEventResult)),
                errors => Problem(errors));

        }

        [HttpDelete("{inGameEventOrderId}")]
        public async Task<IActionResult> DeleteInGameEventOrderAsync([FromRoute] Guid inGameEventOrderId)
        {
            var command = _mapper.Map<DeleteInGameEventOrderCommand>(inGameEventOrderId);

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                result => NoContent(),
                errors => Problem(errors));

        }

        [HttpPatch("{inGameEventOrderId}")]
        public async Task<IActionResult> UpdateInGameEventOrderAsync([FromRoute] Guid inGameEventOrderId, [FromBody] UpdateInGameEventOrderRequest request)
        {
            var command = _mapper.Map<UpdateInGameEventOrderCommand>((inGameEventOrderId, request));

            var commandResult = await _mediator.Send(command);

            if (commandResult is null)
            {
                return NoContent();
            }

            return commandResult.Value.Match(
                result => Ok(_mapper.Map<UpdateInGameEventOrderResponse>(result)),
                errors => Problem(errors));

        }
    }
}
