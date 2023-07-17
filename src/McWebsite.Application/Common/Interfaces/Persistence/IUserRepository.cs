using ErrorOr;
using McWebsite.Domain.User.ValueObjects;
using McWebsite.Domain.User;

namespace McWebsite.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<ErrorOr<IEnumerable<User>>> GetUsers(int page, int entriesPerPage);
        Task<ErrorOr<User>> GetUser(UserId userId);
    }
}
