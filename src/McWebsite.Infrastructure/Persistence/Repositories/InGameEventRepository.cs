using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.InGameEvent.ValueObjects;
using McWebsite.Domain.InGameEvent;
using McWebsite.Infrastructure.Exceptions;
using McWebsite.Domain.InGameEvent.Entities;
using Microsoft.EntityFrameworkCore;
using McWebsite.Domain.Common.Errors;

namespace McWebsite.Infrastructure.Persistence.Repositories
{
    internal sealed class InGameEventRepository : IInGameEventRepository
    {
        private readonly McWebsiteDbContext _dbContext;

        public InGameEventRepository(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<IEnumerable<InGameEvent>>> GetInGameEvents(int page, int entriesPerPage)
        {
            return await _dbContext.InGameEvents
                .OrderBy(p => p.CreatedDateTime)
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage)
                .ToListAsync();
        }
        public async Task<ErrorOr<InGameEvent>> GetInGameEvent(InGameEventId inGameEventId)
        {
            var inGameEvent = await _dbContext.InGameEvents.FirstOrDefaultAsync(gs => gs.Id == inGameEventId);

            if (inGameEvent is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return inGameEvent;
        }

        public async Task<ErrorOr<InGameEvent>> CreateInGameEvent(InGameEvent inGameEvent)
        {
            _dbContext.InGameEvents.Add(inGameEvent);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowCreationException();
            }

            return inGameEvent;
        }

        public async Task DeleteInGameEvent(InGameEvent inGameEvent)
        {
            _dbContext.Remove(inGameEvent);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowDeletionException();
            }
        }


        public async Task<ErrorOr<InGameEvent>> UpdateInGameEvent(InGameEvent inGameEvent)
        {
            _dbContext.ChangeTracker.Clear();
            var updatedInGameEvent = _dbContext.InGameEvents.Update(inGameEvent);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowUpdateException();
            }

            return updatedInGameEvent.Entity;
        }
    }
}
