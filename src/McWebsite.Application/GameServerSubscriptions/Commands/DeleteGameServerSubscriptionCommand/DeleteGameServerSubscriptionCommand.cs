using ErrorOr;
using MediatR;

namespace McWebsite.Application.GameServerSubscriptions.Commands.DeleteGameServerSubscriptionCommand
{
    public sealed record DeleteGameServerSubscriptionCommand(Guid GameServerSubscriptionId) : IRequest<ErrorOr<bool>>;
}
