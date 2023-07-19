using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using McWebsite.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace McWebsite.Infrastructure.Persistence.Repositories
{
    internal sealed class ConversationRepository : IConversationRepository
    {
        private readonly McWebsiteDbContext _dbContext;

        public ConversationRepository(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<IEnumerable<Conversation>>> GetConversations(int page, int entriesPerPage)
        {
            return await _dbContext.Conversations
                .OrderBy(p => p.CreatedDateTime)
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage)
                .ToListAsync();
        }
        public async Task<ErrorOr<Conversation>> GetConversation(ConversationId conversationId)
        {
            var conversation = await _dbContext.Conversations.FirstOrDefaultAsync(gs => gs.Id == conversationId);

            if (conversation is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return conversation;
        }

        public async Task<ErrorOr<Conversation>> GetConversation(UserId FirstParticipantId, UserId SecondParticipantId)
        {
            var conversation = await _dbContext.Conversations.FirstOrDefaultAsync(gs => gs.Participants.FirstParticipantId == FirstParticipantId
            && gs.Participants.SecondParticipantId == SecondParticipantId);

            if (conversation is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return conversation;
        }

        public async Task<ErrorOr<Conversation>> CreateConversation(Conversation conversation)
        {
            _dbContext.Conversations.Add(conversation);

            int result = await _dbContext.SaveChangesAsync();

            if(await _dbContext.Conversations.FirstOrDefaultAsync(c => c.Id == conversation.Id) is not Conversation)
            {
                ExceptionsList.ThrowCreationException();
            }

            return conversation;
        }

        public async Task DeleteConversation(Conversation conversation)
        {
            _dbContext.Remove(conversation);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowDeletionException();
            }
        }


        public async Task<ErrorOr<Conversation>> UpdateConversation(Conversation conversation)
        {
            _dbContext.ChangeTracker.Clear();
            var updatedConversation = _dbContext.Conversations.Update(conversation);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowUpdateException();
            }

            return updatedConversation.Entity;
        }
    }

}
