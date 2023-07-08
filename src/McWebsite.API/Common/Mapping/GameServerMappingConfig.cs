using Mapster;
using McWebsite.API.Contracts.GameServer;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Queries.GetGameServer;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using McWebsite.Domain.GameServer;

namespace McWebsite.API.Common.Mapping
{
    public class GameServerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            RegisterQueriesCommandsMapping(config);

            config.NewConfig<GameServer, GetGameServerResponse>()
                .ConstructUsing(src => new GetGameServerResponse(src.Id.Value,
                                                                 src.MaximumPlayersNumber,
                                                                 src.CurrentPlayersNumber,
                                                                 src.ServerLocation.Value.ToString(),
                                                                 src.ServerType.Value.ToString(),
                                                                 src.Description));

            config.NewConfig<GetGameServersResult, GetGameServersResponse>()
                .BeforeMapping((src, dest) => Console.WriteLine((src,dest)))
                .Map(dest => dest.GameServers, src => src.GameServers.Select(gs=>gs.Adapt<GetGameServerResponse>()))
                .MapToConstructor(true);

        }

        public void RegisterQueriesCommandsMapping(TypeAdapterConfig config)
        {

            config.NewConfig<Guid, GetGameServerQuery>()
                .Map(dest => dest.GameServerId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(int page, int entriesPerPage), GetGameServersQuery>()
              .Map(dest => dest.Page, src => src.page)
              .Map(dest => dest.EntriesPerPage, src => src.entriesPerPage)
              .MapToConstructor(true);

            config.NewConfig<CreateGameServerRequest, CreateGameServerCommand>()
                .Map(dest => dest, src => src)
                .MapToConstructor(true);
        }

    }
}
