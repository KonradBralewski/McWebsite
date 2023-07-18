using McWebsite.Domain.Message.Events;
using MediatR;

namespace McWebsite.Application.Messages.Events
{
    public sealed class MessageUpdatedEventHandler : INotificationHandler<MessageUpdatedEvent>
    {
        public Task Handle(MessageUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
