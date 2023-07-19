using ErrorOr;
using McWebsite.Application.Common.Interfaces.DomainIntegration;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.MessageModel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Persistence.Integration
{
    public sealed class ConversationAndMessagesIntegration : IConversationAndMessagesIntegration
    {
        private readonly McWebsiteDbContext _dbContext;
        public ConversationAndMessagesIntegration(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<ErrorOr<bool>> AddMessageToConversation(ConversationId conversationId, MessageId messageId)
        {
            var conversation = await _dbContext.Conversations.FirstOrDefaultAsync(c => c.Id == conversationId);
            
            if (conversation is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            _dbContext.Attach(conversation);
            var entityEntry = _dbContext.Entry(conversation);

            if (conversation.MessageIds.Any(mi => mi == messageId))
            {
                return false; // refactor with predefined exception, should never happen
            }

            IReadOnlyCollection<MessageId> messageIds = conversation.MessageIds.Append(messageId).ToList().AsReadOnly();

            var messageIdsCollection = entityEntry.Collection(x => x.MessageIds);

            messageIdsCollection.IsModified = true;

            messageIdsCollection.CurrentValue = messageIds;

            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<ErrorOr<bool>> AddMessageToNotExistingYetConversation(ConversationId conversationId, MessageId messageId)
        {
            var entityEntry = _dbContext.ChangeTracker.Entries<Conversation>()
                .Where(x => x.State == EntityState.Added && x.Entity.Id.Value.ToString() == conversationId.Value.ToString()).FirstOrDefault();

            if(entityEntry is null)
            {
                return false; // refactor with predefined exception, should never happen
            }

            if(entityEntry!.Entity.MessageIds.Any(mi => mi == messageId))
            {
                return false; // refactor with predefined exception, should never happen
            }

            Conversation conversation = entityEntry.Entity;

            IReadOnlyCollection<MessageId> messageIds = conversation.MessageIds.Append(messageId).ToList().AsReadOnly();

            var messageIdsCollection = entityEntry.Collection(x => x.MessageIds);

            messageIdsCollection.IsModified = true;

            messageIdsCollection.CurrentValue = messageIds;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
