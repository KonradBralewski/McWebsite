using McWebsite.Domain.GameServer;
using McWebsite.Domain.Message.Entities;

namespace McWebsite.Application.Messages.Commands.CreateMessageCommand
{
    public sealed record CreateMessageResult(Message Message);
}
