namespace McWebsite.API.Contracts.Conversation
{
    public sealed record GetConversationsResponse(IEnumerable<SingleConversationEntry> Conversations);

    public sealed record SingleConversationEntry(Guid Id,
                                                 Guid FirstParticipant,
                                                 Guid SecondParticipant,
                                                 IEnumerable<Guid> ConversationMessagesId);
}
