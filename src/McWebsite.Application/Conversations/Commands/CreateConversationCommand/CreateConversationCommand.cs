using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Conversations.Commands.CreateConversationCommand
{
    public sealed record CreateConversationCommand(Guid FirstParticipant,
                                                   Guid SecondParticipant,
                                                   string FirstMessageContent) : IRequest<ErrorOr<CreateConversationResult>>;
}
