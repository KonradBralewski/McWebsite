using ErrorOr;
using MediatR;

namespace McWebsite.Application.GameServerSubscriptions.Queries.GetGameServerSubscriptionQuery
{
    public sealed record GetGameServerSubscriptionQuery(Guid GameServerSubscriptionId) : IRequest<ErrorOr<GetGameServerSubscriptionResult>>;
}
