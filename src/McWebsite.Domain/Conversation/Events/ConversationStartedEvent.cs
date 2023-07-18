using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.User.ValueObjects;

namespace McWebsite.Domain.Conversation.Events
{
    public sealed record ConversationStartedEvent(ConversationId ConversationId,
                                                  ConversationParticipants Participants,
                                                  string FirstMessageContent) : IDomainEvent;
}
