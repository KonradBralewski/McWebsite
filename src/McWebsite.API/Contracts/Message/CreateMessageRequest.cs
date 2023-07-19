using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;

namespace McWebsite.API.Contracts.Message
{
    public sealed record CreateMessageRequest(Guid ReceiverId,
                                              string MessageContent);
}
