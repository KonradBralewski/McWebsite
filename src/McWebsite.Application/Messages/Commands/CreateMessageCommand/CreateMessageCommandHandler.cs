using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.Common.Utilities;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.User.ValueObjects;
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
        private readonly IConversationRepository _conversationRepository;
        public CreateMessageCommandHandler(IMessageRepository messageRepository, IConversationRepository conversationRepository)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
        }
        public async Task<ErrorOr<CreateMessageResult>> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
        {
            var existingConversationSearchResult = await _conversationRepository.GetConversation(UserId.Create(command.ShipperId),
                                                                                                 UserId.Create(command.ReceiverId));

            Guid? conversationId = null;

            if(existingConversationSearchResult.FirstError.Type == ErrorType.NotFound)
            {
                var conversationCreationResult = await _conversationRepository.CreateConversation(Conversation.Create(command.ShipperId,
                                                                                     command.ReceiverId,
                                                                                     DateTime.UtcNow,
                                                                                     DateTime.UtcNow));
                if (conversationCreationResult.IsError)
                {
                    return conversationCreationResult.Errors;
                }

                conversationId = conversationCreationResult.Value.Id.Value;
            }

            conversationId ??= existingConversationSearchResult.Value.Id.Value;

            Message toBeAdded = Message.Create(conversationId.Value,
                                               command.ReceiverId,
                                               command.ShipperId,
                                               command.MessageContent,
                                               DateTime.UtcNow,
                                               DateTime.UtcNow);
            toBeAdded.Create();
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
