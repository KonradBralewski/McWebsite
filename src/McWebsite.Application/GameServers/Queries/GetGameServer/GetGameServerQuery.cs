using ErrorOr;
using McWebsite.Domain.GameServer;
using MediatR;

namespace McWebsite.Application.GameServers.Queries.GetGameServer
{
    public sealed record GetGameServerQuery(Guid GameServerId) : IRequest<ErrorOr<GameServer>>;
}
