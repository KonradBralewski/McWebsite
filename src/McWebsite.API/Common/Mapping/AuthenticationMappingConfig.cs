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
                .ConstructUsing(src => new AuthenticationResponse(
                    src.User.Id.Value,
                    src.User.Email,
                    src.Token));
        }
    }
}
