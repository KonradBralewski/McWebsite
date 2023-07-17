using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Conversation;
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
            var firstParticipantSearchResult = await _userRepository.GetUser(UserId.Create(command.FirstParticipant));

            if (firstParticipantSearchResult.IsError)
            {
                return firstParticipantSearchResult.Errors;
            }

            var secondParticipantSearchResult = await _userRepository.GetUser(UserId.Create(command.SecondParticipant));

            if (secondParticipantSearchResult.IsError)
            {
                return secondParticipantSearchResult.Errors;
            }

            Conversation toBeAdded = Conversation.Create(command.FirstParticipant,
                                                     command.SecondParticipant,
                                                     DateTime.UtcNow,
                                                     DateTime.UtcNow);

            var creationResult = await _conversationRepository.CreateConversation(toBeAdded);

            if (creationResult.IsError)
            {
                return creationResult.Errors;
            }

            Conversation createdConversation = creationResult.Value;

            createdConversation.Start();

            return new CreateConversationResult(createdConversation);
        }
    }
}
