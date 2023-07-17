using ErrorOr;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Commands.DeleteInGameEventOrderCommand
{
    public sealed record DeleteInGameEventOrderCommand(Guid InGameEventOrderId) : IRequest<ErrorOr<bool>>;
}
