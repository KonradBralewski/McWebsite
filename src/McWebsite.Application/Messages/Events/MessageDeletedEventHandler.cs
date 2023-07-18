using McWebsite.Domain.GameServer.Events;
using McWebsite.Domain.Message.Events;
using MediatR;

namespace McWebsite.Application.Messages.Events
{
    public sealed class MessageDeletedEventHandler : INotificationHandler<MessageDeletedEvent>
    {
        public Task Handle(MessageDeletedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
