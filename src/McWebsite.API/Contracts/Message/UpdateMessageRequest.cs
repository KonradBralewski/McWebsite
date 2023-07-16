namespace McWebsite.API.Contracts.Message
{
    public sealed record UpdateMessageRequest(Guid ConversationId,
                                              Guid ReceiverId,
                                              string MessageContent);
}
