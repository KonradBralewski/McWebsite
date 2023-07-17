using McWebsite.Domain.Message.Entities;

namespace McWebsite.Application.Messages.Queries.GetMessagesQuery
{
    public sealed record GetMessagesResult(IEnumerable<Message> Messages);
}
