using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerSubscriptions.Commands.UpdateGameServerSubscriptionCommand
{
    public sealed record UpdateGameServerSubscriptionCommand(Guid GameServerSubscriptionId,
                                                             Guid GameServerId,
                                                             string SubscriptionType,
                                                             int InGameSubscriptionId,
                                                             float Price,
                                                             string SubscriptionDescription,
                                                             TimeSpan SubscriptionDuration) : IRequest<ErrorOr<UpdateGameServerSubscriptionResult>?>;
}
