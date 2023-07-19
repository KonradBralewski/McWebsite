using ErrorOr;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.MessageModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Common.Interfaces.DomainIntegration
{
    public interface IConversationAndMessagesIntegration
    {
        Task<ErrorOr<bool>> AddMessageToConversation(ConversationId conversationId, MessageId messageId);
        Task<ErrorOr<bool>> AddMessageToNotExistingYetConversation(ConversationId conversationId, MessageId messageId);
    }
}
