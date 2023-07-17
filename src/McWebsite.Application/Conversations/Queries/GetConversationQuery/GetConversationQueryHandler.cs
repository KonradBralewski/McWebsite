using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.MessageModel.ValueObjects;
using MediatR;

namespace McWebsite.Application.Conversations.Queries.GetConversationQuery
{
    public sealed class GetConversationQueryHandler : IRequestHandler<GetConversationQuery, ErrorOr<GetConversationResult>>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository _messageRepository;
        public GetConversationQueryHandler(IConversationRepository conversationRepository, IMessageRepository messageRepository)
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
        }
        public async Task<ErrorOr<GetConversationResult>> Handle(GetConversationQuery query, CancellationToken cancellationToken)
        {
            var getConversationResult = await _conversationRepository.GetConversation(ConversationId.Create(query.ConversationId));

            if (getConversationResult.IsError)
            {
                return getConversationResult.Errors;
            }

            Conversation conversation = getConversationResult.Value;

            var conversationMessagesResult = await _messageRepository.GetConversationMessages(ConversationId.Create(conversation.Id.Value));

            if (conversationMessagesResult.IsError)
            {
                return conversationMessagesResult.Errors;
            }

            return new GetConversationResult(conversation, conversationMessagesResult.Value);
        }
    }
}
