using McWebsite.Domain.GameServer.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Messages.Events
{
    public sealed class MessageUpdatedEventHandler : INotificationHandler<GameServerUpdated>
    {
        public Task Handle(GameServerUpdated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
