namespace McWebsite.API.Contracts.Message
{
    public sealed record UpdateMessageResponse(Guid Id,
                                               Guid ConversationId,
                                               Guid ReceiverId,
                                               Guid ShipperId,
                                               string MessageContent,
                                               DateTime UpdatedDateTime);
}
