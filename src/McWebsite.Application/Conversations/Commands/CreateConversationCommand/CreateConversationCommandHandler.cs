using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.Exceptions;
using McWebsite.Domain.Common.Errors.SystemUnexpected;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using MediatR;

namespace McWebsite.Application.Conversations.Commands.CreateConversationCommand
{
    public sealed class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, ErrorOr<CreateConversationResult>>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserRepository _userRepository;
        public CreateConversationCommandHandler(IConversationRepository conversationRepository, IUserRepository userRepository)
        {
            _conversationRepository = conversationRepository;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<CreateConversationResult>> Handle(CreateConversationCommand command, CancellationToken cancellationToken)
        {
            var alreadyExistingConversationSearchResult = await _conversationRepository.GetConversation(
                UserId.Create(command.FirstParticipantId),
                UserId.Create(command.SecondParticipantId));

            if (!alreadyExistingConversationSearchResult.IsError)
            {
                ExceptionsList.ThrowAlreadyExistingUnitCreationTryException();
            }

            if(alreadyExistingConversationSearchResult.IsError 
                && alreadyExistingConversationSearchResult.FirstError.Type != ErrorType.NotFound)
            {
                return alreadyExistingConversationSearchResult.Errors;
            }

            var firstParticipantSearchResult = await _userRepository.GetUser(UserId.Create(command.FirstParticipantId));

            if (firstParticipantSearchResult.IsError)
            {
                return firstParticipantSearchResult.Errors;
            }

            var secondParticipantSearchResult = await _userRepository.GetUser(UserId.Create(command.SecondParticipantId));

            if (secondParticipantSearchResult.IsError)
            {
                return secondParticipantSearchResult.Errors;
            }


            Conversation toBeAdded = Conversation.Create(command.FirstParticipantId,
                                                     command.SecondParticipantId,
                                                     DateTime.UtcNow,
                                                     DateTime.UtcNow);

            toBeAdded.Start(command.FirstMessageContent);
            var creationResult = await _conversationRepository.CreateConversation(toBeAdded);

            if (creationResult.IsError)
            {
                return creationResult.Errors;
            }

            Conversation createdConversation = creationResult.Value;

            return new CreateConversationResult(createdConversation);
        }
    }
}
