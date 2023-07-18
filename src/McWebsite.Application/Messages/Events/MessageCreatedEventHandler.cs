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
        public MessageCreatedEventHandler(IConversationRepository conversationRepository, IMessageRepository messageRepository)
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
        }
        public async Task Handle(MessageCreatedEvent notification, CancellationToken cancellationToken)
        {
  
            Conversation conversation = conversationSearchResult.Value;

            Conversation conversationUpdatedWithNewMessage = Conversation.Recreate(conversation.Id.Value,
                                                                                   conversation.Participants.FirstParticipantId.Value,
                                                                                   conversation.Participants.SecondParticipantId.Value,
                                                                                   conversation.MessageIds.Select(mi => mi.Value),
                                                                                   conversation.CreatedDateTime,
                                                                                   DateTime.UtcNow);

            await _conversationRepository.UpdateConversation(conversationUpdatedWithNewMessage);

            return;
        }
    }
}
