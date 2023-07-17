using ErrorOr;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrderQuery
{
    public sealed record GetInGameEventOrderQuery(Guid InGameEventOrderId) : IRequest<ErrorOr<GetInGameEventOrderResult>>;
}
