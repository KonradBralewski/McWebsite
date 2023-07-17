using McWebsite.Domain.GameServer.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Events
{
    public sealed class GameServerDeletedEventHandler : INotificationHandler<GameServerDeletedEvent>
    {
        public Task Handle(GameServerDeletedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
