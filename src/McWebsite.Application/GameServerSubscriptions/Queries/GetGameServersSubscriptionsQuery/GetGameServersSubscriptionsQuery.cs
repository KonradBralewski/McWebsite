using ErrorOr;
using MediatR;

namespace McWebsite.Application.GameServerSubscriptions.Queries.GetGameServersSubscriptionsQuery
{
    public sealed record GetGameServersSubscriptionsQuery(int Page, int EntriesPerPage) : IRequest<ErrorOr<GetGameServersSubscriptionsResult>>;
}
