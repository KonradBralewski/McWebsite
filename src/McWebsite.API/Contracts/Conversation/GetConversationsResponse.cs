namespace McWebsite.API.Contracts.Conversation
{
    public sealed record GetConversationsResponse(IEnumerable<GetConversationResponse> Conversations);
}
