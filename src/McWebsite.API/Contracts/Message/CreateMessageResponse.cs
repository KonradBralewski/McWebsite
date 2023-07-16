namespace McWebsite.API.Contracts.Message
{
    public sealed record CreateMessageResponse(Guid Id,
                                               Guid ConversationId,
                                               Guid ReceiverId,
                                               Guid ShipperId,
                                               string MessageContent,
                                               DateTime SentDateTime);
}
