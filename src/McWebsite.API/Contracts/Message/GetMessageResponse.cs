namespace McWebsite.API.Contracts.Message
{
    public sealed record GetMessageResponse(Guid Id,
                                            Guid ConversationId,
                                            Guid ReceiverId,
                                            Guid ShipperId,
                                            string MessageContent);
}
