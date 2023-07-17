using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Messages.Commands.DeleteMessageCommand
{
    public sealed record DeleteMessageCommand(Guid MessageId) : IRequest<ErrorOr<bool>>;
}
