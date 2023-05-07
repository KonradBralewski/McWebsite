using McWebsite.Domain.Entities;

namespace McWebsite.Application.Services.Authentication
{
    public record AuthenticationResult (User user, string Token);
   
}