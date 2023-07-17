using McWebsite.Domain.Common.DomainBase;

namespace McWebsite.Domain.Conversation.Events
{
    public sealed record ConversationStartedEvent() : IDomainEvent;
}
