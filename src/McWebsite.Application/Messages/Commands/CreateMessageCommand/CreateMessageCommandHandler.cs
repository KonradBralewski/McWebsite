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
        private readonly IUserRepository _userRepository;
        public CreateMessageCommandHandler(IMessageRepository messageRepository, IConversationRepository conversationRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<CreateMessageResult>> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
        {
            Guid? conversationId = null;

            var conversationSearchResult = await FindExistingConversation(command.ShipperId, command.ReceiverId);

            if(conversationSearchResult.IsError)
            {
                if(conversationSearchResult.FirstError.Type != ErrorType.NotFound)
                {
                    return conversationSearchResult.Errors;
                }

                var newConversation = await CreateNewConversation(command.ShipperId, command.ReceiverId);

                if(newConversation.IsError)
                {
                    return newConversation.Errors;
                }

                conversationId = newConversation.Value.Id.Value;
            }

            var participantsSearchResult = await CheckIfParticipantsExists(command.ShipperId, command.ReceiverId);

            if(participantsSearchResult.IsError)
            {
                return participantsSearchResult.Errors;
            }

            conversationId ??= conversationSearchResult.Value.Id.Value;

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

        private async Task<ErrorOr<Conversation>> FindExistingConversation(Guid shipperId, Guid receiverId)
        {
            var existingConversationSearchResult = await _conversationRepository.GetConversation(UserId.Create(shipperId),
                                                                                               UserId.Create(receiverId));

            if (existingConversationSearchResult.IsError)
            {
                return existingConversationSearchResult.Errors;
            }

            return existingConversationSearchResult;
        }

        private async Task<ErrorOr<Conversation>> CreateNewConversation(Guid shipperId, Guid receiverId)
        {
            Conversation conversation = Conversation.Create(receiverId,
                                                            shipperId,
                                                            DateTime.UtcNow,
                                                            DateTime.UtcNow);

            var conversationCreationResult = await _conversationRepository.CreateConversation(conversation);

            if (conversationCreationResult.IsError)
            {
                return conversationCreationResult.Errors;
            }

            return conversationCreationResult;
        }

        private async Task<ErrorOr<bool>> CheckIfParticipantsExists(Guid shipperId, Guid receiverId)
        {
            var shipperSearchResult = await _userRepository.GetUser(UserId.Create(shipperId));

            if (shipperSearchResult.IsError)
            {
                return shipperSearchResult.Errors;
            }

            var receiverSearchResult = await _userRepository.GetUser(UserId.Create(receiverId));

            if (receiverSearchResult.IsError)
            {
                return receiverSearchResult.Errors;
            }

            return true;
        }
    }
}
