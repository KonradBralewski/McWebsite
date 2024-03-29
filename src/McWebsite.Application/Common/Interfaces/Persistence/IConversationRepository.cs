﻿using ErrorOr;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.User.ValueObjects;

namespace McWebsite.Application.Common.Interfaces.Persistence
{
    public interface IConversationRepository
    {
        Task<ErrorOr<IEnumerable<Conversation>>> GetConversations(int page, int entriesPerPage);
        Task<ErrorOr<Conversation>> GetConversation(ConversationId conversationId);
        Task<ErrorOr<Conversation>> GetConversation(UserId FirstParticipantId, UserId SecondParticipantId);
        Task<ErrorOr<Conversation>> CreateConversation(Conversation conversation);
        Task<ErrorOr<Conversation>> UpdateConversation(Conversation conversation);
        Task DeleteConversation(Conversation conversation);
    }
}
