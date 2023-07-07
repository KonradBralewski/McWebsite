using Mapster;
using McWebsite.API.Contracts.GameServer;
using McWebsite.Application.GameServers.Queries.GetGameServer;
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

            config.NewConfig<GameServer, GetGameServerResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.MaximumPlayersNumber, src => src.MaximumPlayersNumber)
                .Map(dest => dest.CurrentPlayersNumber, src => src.CurrentPlayersNumber)
                .Map(dest => dest.ServerLocation, src => src.ServerLocation.Value.ToString())
                .Map(dest => dest.ServerType, src => src.ServerType.Value.ToString())
                .Map(dest => dest.Description, src => src.Description)
                .MapToConstructor(true);

            config.NewConfig<GetGameServersResult, GetGameServersResponse>()
                .Map(dest => dest.GameServers, src => src.GameServers.Select(gs=>gs.Adapt<GetGameServerResponse>()))
                .MapToConstructor(true);


            config.NewConfig<Guid, GetGameServerQuery>()
                .BeforeMapping((dest, src) => Console.WriteLine((dest, src)))
                .Map(dest => dest.GameServerId, src => src)
                .MapToConstructor(true);
                
        }
    }
}
