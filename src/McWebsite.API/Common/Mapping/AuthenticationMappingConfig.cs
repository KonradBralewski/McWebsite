using Mapster;
using McWebsite.API.Contracts.Auth;
using McWebsite.Application.Authentication;

namespace McWebsite.API.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.UserId, src => src.User.Id)
                .Map(dest => dest.UserEmail, src => src.User.Email.Value)
                .Map(dest => dest.Token, src => src.Token);
        }
    }
}
