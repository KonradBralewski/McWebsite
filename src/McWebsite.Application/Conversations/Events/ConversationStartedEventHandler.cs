using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Conversation.Events;
using McWebsite.Domain.Message.Entities;
using MediatR;

namespace McWebsite.Application.Conversations.Events
{
    internal class ConversationStartedEventHandler : INotificationHandler<ConversationStartedEvent>
    {
        private readonly IMessageRepository _messageRepository;
        public ConversationStartedEventHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task Handle(ConversationStartedEvent notification, CancellationToken cancellationToken)
        {
            Message firstMessage = Message.Create(notification.ConversationId.Value,
                                                  notification.Participants.SecondParticipantId.Value,
                                                  notification.Participants.FirstParticipantId.Value,
                                                  notification.FirstMessageContent,
                                                  DateTime.UtcNow,
                                                  DateTime.UtcNow);

            firstMessage.Create();

            await _messageRepository.CreateMessage(firstMessage);
            // Unexpected creation failure already handled in repository by throwing predefined exception.
            return;
        }
    }
}
