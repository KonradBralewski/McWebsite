using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.MessageModel.ValueObjects;
using McWebsite.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace McWebsite.Infrastructure.Persistence.Repositories
{
    internal sealed class MessageRepository : IMessageRepository
    {
        private readonly McWebsiteDbContext _dbContext;

        public MessageRepository(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ErrorOr<IEnumerable<Message>>> GetConversationMessages(ConversationId conversationId)
        {
            return await _dbContext.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(p => p.SentDateTime)
                .ToListAsync();
        }


        public async Task<ErrorOr<IEnumerable<Message>>> GetMessages(int page, int entriesPerPage)
        {
            return await _dbContext.Messages
                .OrderBy(p => p.SentDateTime)
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage)
                .ToListAsync();
        }
        public async Task<ErrorOr<Message>> GetMessage(MessageId messageId)
        {
            var message = await _dbContext.Messages.FirstOrDefaultAsync(gs => gs.Id == messageId);

            if (message is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return message;
        }

        public async Task<ErrorOr<Message>> CreateMessage(Message message)
        {
            _dbContext.Messages.Add(message);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowCreationException();
            }

            return message;
        }

        public async Task DeleteMessage(Message message)
        {
            _dbContext.Remove(message);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowDeletionException();
            }
        }


        public async Task<ErrorOr<Message>> UpdateMessage(Message message)
        {
            _dbContext.ChangeTracker.Clear();
            var updatedMessage = _dbContext.Messages.Update(message);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowUpdateException();
            }

            return updatedMessage.Entity;
        }
    }
}
