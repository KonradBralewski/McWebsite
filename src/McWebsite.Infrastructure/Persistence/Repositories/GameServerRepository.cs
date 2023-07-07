using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<GameServer> GetGameServer(GameServerId gameServerId)
        {
            return await _dbContext.GameServers.FirstOrDefaultAsync(gs => gs.Id == gameServerId);
        }

        public async Task<ErrorOr<GameServer>> CreateGameServer(GameServer gameServer)
        {
            _dbContext.GameServers.Add(gameServer);

            int result = await _dbContext.SaveChangesAsync();

            if(result == 0)
            {
                return Errors.Persistence.UnitCreationError;
            }

            return gameServer;
        }

        public Task DeleteGameServer(GameServer gameServer)
        {
            throw new NotImplementedException();
        }


        public Task<GameServer> UpdateGameServer(GameServer gameServer)
        {
            throw new NotImplementedException();
        }
    }
}
