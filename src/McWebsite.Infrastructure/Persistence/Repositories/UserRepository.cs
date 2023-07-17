using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.User;
using McWebsite.Domain.User.ValueObjects;
using McWebsite.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace McWebsite.Infrastructure.Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly McWebsiteDbContext _dbContext;

        public UserRepository(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<IEnumerable<User>>> GetUsers(int page, int entriesPerPage)
        {
            return await _dbContext.DomainUsers
                .OrderBy(p => p.CreatedDateTime)
                .Skip(page * entriesPerPage)
                .Take(entriesPerPage)
                .ToListAsync();
        }
        public async Task<ErrorOr<User>> GetUser(UserId userId)
        {
            var user = await _dbContext.DomainUsers.FirstOrDefaultAsync(gs => gs.Id == userId);

            if(user is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return user;
        }
    }
}
