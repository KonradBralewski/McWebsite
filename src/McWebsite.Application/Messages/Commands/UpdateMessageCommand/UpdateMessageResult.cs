using McWebsite.Domain.Message.Entities;

namespace McWebsite.Application.Messages.Commands.UpdateMessageCommand
{
    public sealed record UpdateMessageResult(Message message);
}
