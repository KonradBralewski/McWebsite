using ErrorOr;
using McWebsite.Domain.User;

namespace McWebsite.Application.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<User?> GetUserByEmail(string email);
        Task<ErrorOr<User>> AddUser(User user);
        Task<ErrorOr<bool>> DoCredentialsMatch(string email, string password);
    }
}
