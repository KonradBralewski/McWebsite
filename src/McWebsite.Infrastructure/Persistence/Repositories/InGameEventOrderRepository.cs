using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.InGameEventOrder;
using McWebsite.Domain.InGameEventOrder.ValueObjects;
using McWebsite.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace McWebsite.Infrastructure.Persistence.Repositories
{
    internal sealed class InGameEventOrderRepository : IInGameEventOrderRepository
    {
        private readonly McWebsiteDbContext _dbContext;

        public InGameEventOrderRepository(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<IEnumerable<InGameEventOrder>>> GetInGameEventOrders(int page, int entriesPerPage)
        {
            return await _dbContext.InGameEventOrders
                .OrderBy(p => p.OrderDate)
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage)
                .ToListAsync();
        }
        public async Task<ErrorOr<InGameEventOrder>> GetInGameEventOrder(InGameEventOrderId inGameEventOrderId)
        {
            var inGameEventOrder = await _dbContext.InGameEventOrders.FirstOrDefaultAsync(gs => gs.Id == inGameEventOrderId);

            if (inGameEventOrder is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return inGameEventOrder;
        }

        public async Task<ErrorOr<InGameEventOrder>> CreateInGameEventOrder(InGameEventOrder inGameEventOrder)
        {
            _dbContext.InGameEventOrders.Add(inGameEventOrder);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowCreationException();
            }

            return inGameEventOrder;
        }

        public async Task DeleteInGameEventOrder(InGameEventOrder inGameEventOrder)
        {
            _dbContext.Remove(inGameEventOrder);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowDeletionException();
            }
        }


        public async Task<ErrorOr<InGameEventOrder>> UpdateInGameEventOrder(InGameEventOrder inGameEventOrder)
        {
            _dbContext.ChangeTracker.Clear();
            var updatedInGameEvent = _dbContext.InGameEventOrders.Update(inGameEventOrder);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowUpdateException();
            }

            return updatedInGameEvent.Entity;
        }
    }
}
