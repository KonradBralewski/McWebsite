using McWebsite.Application.Common.Interfaces.DomainIntegration;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.Message.Events;
using MediatR;
using Serilog;

namespace McWebsite.Application.Messages.Events
{
    public sealed class MessageCreatedEventHandler : INotificationHandler<MessageCreatedEvent>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationAndMessagesIntegration _conversationAndMessagesIntegration;
        public MessageCreatedEventHandler(IConversationRepository conversationRepository,
                                          IMessageRepository messageRepository,
                                          IConversationAndMessagesIntegration conversationAndMessagesIntegration)
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
            _conversationAndMessagesIntegration = conversationAndMessagesIntegration;
        }
        public async Task Handle(MessageCreatedEvent notification, CancellationToken cancellationToken)
        {
            var updatedResult = await _conversationAndMessagesIntegration.AddMessageToConversation(notification.ConversationId, notification.MessageId);

            if (updatedResult.FirstError.Type == ErrorOr.ErrorType.NotFound)
            {
                await _conversationAndMessagesIntegration.AddMessageToNotExistingYetConversation(notification.ConversationId, notification.MessageId);
                return;
            }

            if (updatedResult.IsError)
            {
                Log.Error("MessageCreatedEventHandler integration failure, errors = {Errors}", updatedResult.Errors);
                return;
            }

            return;
        }
    }
}
