using ErrorOr;
using MediatR;

namespace McWebsite.Application.GameServerSubscriptions.Commands.CreateGameServerSubscriptionCommand
{
    public sealed record CreateGameServerSubscriptionCommand(Guid GameServerId,
                                                             string SubscriptionType,
                                                             int InGameSubscriptionId,
                                                             float Price,
                                                             string SubscriptionDescription,
                                                             TimeSpan SubscriptionDuration) : IRequest<ErrorOr<CreateGameServerSubscriptionResult>>;
}
