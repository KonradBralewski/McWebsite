using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using McWebsite.Domain.Common.Errors;

namespace McWebsite.Infrastructure.Persistence.Repositories
{
    internal sealed class GameServerSubscriptionRepository : IGameServerSubscriptionRepository
    {
        private readonly McWebsiteDbContext _dbContext;

        public GameServerSubscriptionRepository(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ErrorOr<IEnumerable<GameServerSubscription>>> GetGameServersSubscriptions(int page, int entriesPerPage)
        {
            return await _dbContext.GameServerSubscriptions
                .OrderBy(p => p.CreatedDateTime)
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage).ToListAsync();
        }
        public async Task<ErrorOr<GameServerSubscription>> GetGameServerSubscription(GameServerSubscriptionId gameServerSubscriptionId)
        {
            var gameServerSubscription = await _dbContext.GameServerSubscriptions.FirstOrDefaultAsync(gs => gs.Id == gameServerSubscriptionId);

            if (gameServerSubscription is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return gameServerSubscription;
        }

        public async Task<ErrorOr<GameServerSubscription>> CreateGameServerSubscription(GameServerSubscription gameServerSubscription)
        {
            _dbContext.GameServerSubscriptions.Add(gameServerSubscription);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowCreationException();
            }

            return gameServerSubscription;
        }

        public async Task DeleteGameServerSubscription(GameServerSubscription gameServerSubscription)
        {
            _dbContext.Remove(gameServerSubscription);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowDeletionException();
            }
        }


        public async Task<ErrorOr<GameServerSubscription>> UpdateGameServerSubscription(GameServerSubscription gameServerSubscription)
        {
            _dbContext.ChangeTracker.Clear();
            var updatedGameServerSubscription = _dbContext.GameServerSubscriptions.Update(gameServerSubscription);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowUpdateException();
            }

            return updatedGameServerSubscription.Entity;
        }
    }
}
