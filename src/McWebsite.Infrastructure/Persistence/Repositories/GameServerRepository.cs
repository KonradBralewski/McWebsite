using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServer;
using McWebsite.Infrastructure.Exceptions;
using McWebsite.Infrastructure.ExceptionsList;
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


        public async Task<IEnumerable<GameServer>> GetGameServers(int page, int entriesPerPage)
        {
            return await _dbContext.GameServers
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage)
                .OrderByDescending(p =>p.CreatedDateTime)
                .ToListAsync();
        }
        public async Task<GameServer> GetGameServer(Guid gameServerId)
        {
            return await _dbContext.GameServers.FirstOrDefaultAsync(gs => gs.Id.Value == gameServerId);
        }

        public async Task<GameServer> CreateGameServer(GameServer gameServer)
        {
            _dbContext.GameServers.Add(gameServer);

            int result = await _dbContext.SaveChangesAsync();

            if(result == 0)
            {
                ExceptionsList.ThrowCreationException;
            }

            return gameServer;
        }

        public Task DeleteGameServer(Guid gameServer)
        {
            throw new NotImplementedException();
        }


        public Task<GameServer> UpdateGameServer(GameServer gameServer)
        {
            throw new NotImplementedException();
        }
    }
}
