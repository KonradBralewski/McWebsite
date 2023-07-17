using McWebsite.Domain.Conversation;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.Message.Entities;

namespace McWebsite.Application.Conversations.Queries.GetConversationQuery
{
    public sealed record GetConversationResult(Conversation Conversation, IEnumerable<Message> ConversationMessages);
}
