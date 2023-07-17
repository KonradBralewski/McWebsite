using ErrorOr;
using McWebsite.Application.GameServers.Queries.GetGameServerQuery;
using McWebsite.Domain.GameServer;
using MediatR;

namespace McWebsite.Application.Messages.Queries.GetMessageQuery
{
    public sealed record GetMessageQuery(Guid MessageId) : IRequest<ErrorOr<GetMessageResult>>;
}
