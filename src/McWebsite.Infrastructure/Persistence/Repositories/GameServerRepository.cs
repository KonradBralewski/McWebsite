using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace McWebsite.Infrastructure.Persistence.Repositories
{
    internal sealed class GameServerRepository : IGameServerRepository
    {
        private readonly McWebsiteDbContext _dbContext;

        public GameServerRepository(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ErrorOr<IEnumerable<GameServer>>> GetGameServers(int page, int entriesPerPage)
        {
            return await _dbContext.GameServers
                .OrderByDescending(p => p.CreatedDateTime)
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage)
                .ToListAsync();
        }
        public async Task<ErrorOr<GameServer>> GetGameServer(GameServerId gameServerId)
        {
            var gameServer = await _dbContext.GameServers.FirstOrDefaultAsync(gs => gs.Id == gameServerId);

            if(gameServer is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return gameServer;
        }

        public async Task<ErrorOr<GameServer>> CreateGameServer(GameServer gameServer)
        {
            _dbContext.GameServers.Add(gameServer);

            int result = await _dbContext.SaveChangesAsync();

            if(result == 0)
            {
                ExceptionsList.ThrowCreationException();
            }

            return gameServer;
        }

        public async Task DeleteGameServer(GameServer gameServer)
        {
            _dbContext.Remove(gameServer);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowDeletionException();
            }
        }


        public async Task<ErrorOr<GameServer>> UpdateGameServer(GameServer gameServer)
        {
            _dbContext.ChangeTracker.Clear();
            _dbContext.GameServers.Update(gameServer);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowUpdateException();
            }

            return gameServer;
        }
    }
}
