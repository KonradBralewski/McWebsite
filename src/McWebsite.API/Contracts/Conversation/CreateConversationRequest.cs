using McWebsite.API.Contracts.Message;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;

namespace McWebsite.API.Contracts.Conversation
{
    public sealed record CreateConversationRequest(Guid OtherParticipant,
                                                   string FirstMessageContent);
}
