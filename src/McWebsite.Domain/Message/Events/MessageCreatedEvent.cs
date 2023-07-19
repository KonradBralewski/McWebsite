using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.MessageModel.ValueObjects;

namespace McWebsite.Domain.Message.Events
{
    public sealed record MessageCreatedEvent(ConversationId ConversationId, MessageId MessageId) : IDomainEvent;
}
