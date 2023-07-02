using Mapster;
using McWebsite.API.Contracts.GameServer;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using McWebsite.Domain.GameServer;

namespace McWebsite.API.Common.Mapping
{
    public class GameServerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(int page, int entriesPerPage), GetGameServersQuery>()
                .Map(dest => dest.Page, src => src.page)
                .Map(dest => dest.EntriesPerPage, src => src.entriesPerPage);

            config.NewConfig<GameServer, GameServerResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.ServerLocation, src => src.ServerLocation.Value)
                .Map(dest => dest.MaximumPlayersNumber, src => src.MaximumPlayersNumber)
                .Map(dest => dest.ServerType, src => src.ServerType.Value)
                .Map(dest => dest.Description, src => src.Description);

            config.NewConfig<GetGameServersResult, GetGameServersResponse>()
                .Map(dest => dest.GameServers, src => src.GameServers.Adapt<GameServerResponse>());
                
        }
    }
}
