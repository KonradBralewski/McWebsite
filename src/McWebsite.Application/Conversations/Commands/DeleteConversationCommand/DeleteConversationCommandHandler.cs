using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.GameServerReport.ValueObjects;
using MediatR;

namespace McWebsite.Application.Conversations.Commands.DeleteConversationCommand
{
    public sealed class DeleteConversationCommandHandler : IRequestHandler<DeleteConversationCommand, ErrorOr<bool>>
    {
        private readonly IConversationRepository _conversationRepository;
        public DeleteConversationCommandHandler(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteConversationCommand command, CancellationToken cancellationToken)
        {
            var conversationSearchResult = await _conversationRepository.GetConversation(ConversationId.Create(command.ConversationId));

            if (conversationSearchResult.IsError)
            {
                return conversationSearchResult.Errors;
            }

            Conversation conversation = conversationSearchResult.Value;

            conversation.Delete();

            await _conversationRepository.DeleteConversation(conversation);

            return true;
        }
    }
}
