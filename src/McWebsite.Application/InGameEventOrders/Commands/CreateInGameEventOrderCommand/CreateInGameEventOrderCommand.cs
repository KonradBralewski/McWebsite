using ErrorOr;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand
{
    public sealed record CreateInGameEventOrderCommand(Guid BuyingUserId,
                                                       Guid BoughtInGameEventId) : IRequest<ErrorOr<CreateInGameEventOrderResult>>;
}
