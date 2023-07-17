using ErrorOr;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand
{
    public sealed record UpdateInGameEventOrderCommand(Guid InGameEventOrderId,
                                                       Guid BoughtInGameEventId) : IRequest<ErrorOr<UpdateInGameEventOrderResult>?>;
}
