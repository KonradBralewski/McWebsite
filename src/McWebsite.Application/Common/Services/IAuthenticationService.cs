using ErrorOr;
using McWebsite.Domain.User;

namespace McWebsite.Application.Common.Services
{
    public interface IAuthenticationService
    {
        Task<User?> GetUserByEmail(string email);
        Task<ErrorOr<User>> AddUser(User user);
    }
}
