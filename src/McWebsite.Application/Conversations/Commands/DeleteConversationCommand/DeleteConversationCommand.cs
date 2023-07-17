using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Conversations.Commands.DeleteConversationCommand
{
    public sealed record DeleteConversationCommand(Guid ConversationId) : IRequest<ErrorOr<bool>>;
}
