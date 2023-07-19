using ErrorOr;
using McWebsite.Domain.GameServer.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Messages.Commands.CreateMessageCommand
{
    public sealed record CreateMessageCommand(Guid ReceiverId,
                                              Guid ShipperId,
                                              string MessageContent) : IRequest<ErrorOr<CreateMessageResult>>;
}
