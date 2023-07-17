using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Messages.Commands.UpdateMessageCommand
{
    public sealed record UpdateMessageCommand(Guid MessageId,
                                              string MessageContent) : IRequest<ErrorOr<UpdateMessageResult>?>;
}
