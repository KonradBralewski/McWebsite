using Mapster;
using McWebsite.API.Contracts.GameServer;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Commands.DeleteGameServerCommand;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Application.GameServers.Queries.GetGameServer;
using McWebsite.Application.GameServers.Queries.GetGameServerQuery;
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
                .Map(dest => dest.GameServers, src => src.GameServers.Select(gs => gs.Adapt<GetGameServerResponse>()))
                .MapToConstructor(true);

            config.NewConfig<GetGameServerResult, GetGameServerResponse>()
                .ConstructUsing(src => src.GameServer.Adapt<GetGameServerResponse>());

            config.NewConfig<UpdateGameServerResult, UpdateGameServerResponse>()
              .ConstructUsing(src => new UpdateGameServerResponse(src.GameServer.Id.Value,
                                                                  src.GameServer.MaximumPlayersNumber,
                                                                  src.GameServer.CurrentPlayersNumber,
                                                                  src.GameServer.ServerLocation.Value.ToString(),
                                                                  src.GameServer.ServerType.Value.ToString(),
                                                                  src.GameServer.Description));

            config.NewConfig<CreateGameServerResult, CreateGameServerResponse>()
                .ConstructUsing(src => new CreateGameServerResponse(src.GameServer.Id.Value,
                                                                    src.GameServer.MaximumPlayersNumber,
                                                                    src.GameServer.ServerLocation.Value.ToString(),
                                                                    src.GameServer.ServerType.Value.ToString(),
                                                                    src.GameServer.Description,
                                                                    src.GameServer.CreatedDateTime));
     

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

            config.NewConfig<Guid, DeleteGameServerCommand>()
                .Map(dest => dest.GameServerId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(Guid GameServerId, UpdateGameServerRequest request), UpdateGameServerCommand>()
                .Map(dest => dest.GameServerId, src => src.GameServerId)
                .Map(dest => dest.MaximumPlayersNumber, src => src.request.MaximumPlayersNumber)
                .Map(dest => dest.CurrentPlayersNumber, src => src.request.CurrentPlayersNumber)
                .Map(dest => dest.ServerLocation, src => src.request.ServerLocation)
                .Map(dest => dest.ServerType, src => src.request.ServerType)
                .Map(dest => dest.Description, src => src.request.Description)
                .MapToConstructor(true);
        }

    }
}
