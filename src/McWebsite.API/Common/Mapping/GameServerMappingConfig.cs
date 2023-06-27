using Mapster;
using McWebsite.API.Contracts.Auth;
using McWebsite.API.Contracts.GameServer;
using McWebsite.Application.Authentication.Commands;
using McWebsite.Application.GameServers.Queries.GetGameServers;

namespace McWebsite.API.Common.Mapping
{
    public class GameServerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(int page, int entriesPerPage), GetGameServersQuery>()
                .Map(dest => dest.page, src => src.page)
                .Map(dest => dest.entriesPerPage, src => src.entriesPerPage);

            config.NewConfig<GetGameServersResult, GetGameServersResponse>()
                .Map(dest => dest, src => src);
        }
    }
}
