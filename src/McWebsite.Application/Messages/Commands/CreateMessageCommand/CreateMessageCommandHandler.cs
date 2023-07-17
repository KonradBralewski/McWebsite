using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.Common.Utilities;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.Message.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Messages.Commands.CreateMessageCommand
{
    public sealed class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, ErrorOr<CreateMessageResult>>
    {
        private readonly IMessageRepository _messageRepository;
        public CreateMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<ErrorOr<CreateMessageResult>> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
        {
            Message toBeAdded = Message.Create(command.ConversationId,
                                               command.ReceiverId,
                                               command.ShipperId,
                                               command.MessageContent,
                                               DateTime.UtcNow,
                                               DateTime.UtcNow);

           var creationResult = await _messageRepository.CreateMessage(toBeAdded);

            if (creationResult.IsError)
            {
                return creationResult.Errors;
            }

            Message createdMessage = creationResult.Value;

            return new CreateMessageResult(createdMessage);
        }
    }
}
