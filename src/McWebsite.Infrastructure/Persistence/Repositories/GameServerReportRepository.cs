using ErrorOr;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServer;
using McWebsite.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.Common.Errors;

namespace McWebsite.Infrastructure.Persistence.Repositories
{
    internal sealed class GameServerReportRepository : IGameServerReportRepository
    {
        private readonly McWebsiteDbContext _dbContext;

        public GameServerReportRepository(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ErrorOr<IEnumerable<GameServerReport>>> GetGameServersReports(int page, int entriesPerPage)
        {
            return await _dbContext.GameServersReports
                .OrderBy(p => p.ReportDate)
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage)
                .ToListAsync();
        }
        public async Task<ErrorOr<GameServerReport>> GetGameServerReport(GameServerReportId gameServerReportId)
        {
            var gameServerReport = await _dbContext.GameServersReports.FirstOrDefaultAsync(gs => gs.Id == gameServerReportId);

            if (gameServerReport is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return gameServerReport;
        }

        public async Task<ErrorOr<GameServerReport>> CreateGameServerReport(GameServerReport gameServerReport)
        {
            _dbContext.GameServersReports.Add(gameServerReport);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowCreationException();
            }

            return gameServerReport;
        }

        public async Task DeleteGameServerReport(GameServerReport gameServerReport)
        {
            _dbContext.Remove(gameServerReport);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowDeletionException();
            }
        }


        public async Task<ErrorOr<GameServerReport>> UpdateGameServerReport(GameServerReport gameServerReport)
        {
            _dbContext.ChangeTracker.Clear();
            var updatedGameServerReport = _dbContext.GameServersReports.Update(gameServerReport);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                ExceptionsList.ThrowUpdateException();
            }

            return updatedGameServerReport.Entity;
        }
    }
}

