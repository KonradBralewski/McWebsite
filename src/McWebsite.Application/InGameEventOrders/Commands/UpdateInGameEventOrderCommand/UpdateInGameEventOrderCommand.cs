using ErrorOr;
using McWebsite.Domain.InGameEvent.ValueObjects;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand
{
    public sealed record UpdateInGameEventOrderCommand(Guid InGameEventId,
                                                       Guid BoughtInGameEventId) : IRequest<ErrorOr<UpdateInGameEventOrderResult>?>;
}
