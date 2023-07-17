using ErrorOr;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrdersQuery
{
    public sealed record GetInGameEventOrdersQuery(int Page, int EntriesPerPage) : IRequest<ErrorOr<GetInGameEventOrdersResult>>;
}
