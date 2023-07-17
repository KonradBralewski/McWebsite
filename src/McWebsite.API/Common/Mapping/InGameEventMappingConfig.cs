using Mapster;
using McWebsite.API.Contracts.InGameEvent;
using McWebsite.Application.InGameEvents.Commands.CreateInGameEventCommand;
using McWebsite.Application.InGameEvents.Commands.DeleteInGameEventCommand;
using McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventQuery;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventsQuery;
using McWebsite.Domain.InGameEvent;
using McWebsite.Domain.InGameEvent.Entities;

namespace McWebsite.API.Common.Mapping
{
    public class InGameEventMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            RegisterQueriesCommandsMapping(config);

            config.NewConfig<InGameEvent, GetInGameEventResponse>()
                .ConstructUsing(src => new GetInGameEventResponse(src.Id.Value,
                                                                  src.GameServerId.Value,
                                                                  src.InGameId,
                                                                  src.InGameEventType.Value.ToString(),
                                                                  src.Description,
                                                                  src.Price));

            config.NewConfig<GetInGameEventsResult, GetInGameEventsResponse>()
                .Map(dest => dest, src => src.InGameEvents.Select(gsr => gsr.Adapt<GetInGameEventResponse>()))
                .MapToConstructor(true);

            config.NewConfig<GetInGameEventResult, GetInGameEventResponse>()
                .ConstructUsing(src => src.InGameEvent.Adapt<GetInGameEventResponse>());

            config.NewConfig<CreateInGameEventResult, CreateInGameEventResponse>()
                .ConstructUsing(src => new CreateInGameEventResponse(src.InGameEvent.Id.Value,
                                                                  src.InGameEvent.GameServerId.Value,
                                                                  src.InGameEvent.InGameId,
                                                                  src.InGameEvent.InGameEventType.Value.ToString(),
                                                                  src.InGameEvent.Description,
                                                                  src.InGameEvent.Price,
                                                                  src.InGameEvent.CreatedDateTime));

            config.NewConfig<UpdateInGameEventResult, UpdateInGameEventResponse>()
                .ConstructUsing(src => new UpdateInGameEventResponse(src.InGameEvent.Id.Value,
                                                                  src.InGameEvent.GameServerId.Value,
                                                                  src.InGameEvent.InGameId,
                                                                  src.InGameEvent.InGameEventType.Value.ToString(),
                                                                  src.InGameEvent.Description,
                                                                  src.InGameEvent.Price,
                                                                  src.InGameEvent.UpdatedDateTime));
        }

        public void RegisterQueriesCommandsMapping(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, GetInGameEventQuery>()
                .Map(dest => dest.InGameEventId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(int page, int entriesPerPage), GetInGameEventsQuery>()
             .Map(dest => dest.Page, src => src.page)
             .Map(dest => dest.EntriesPerPage, src => src.entriesPerPage)
             .MapToConstructor(true);

            config.NewConfig<Guid, DeleteInGameEventCommand>()
                .Map(dest => dest.InGameEventId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(Guid inGameEventId, UpdateInGameEventRequest request), UpdateInGameEventCommand>()
                .Map(dest => dest.InGameEventId, src => src.inGameEventId)
                .Map(dest => dest.GameServerId, src => src.request.GameServerId)
                .Map(dest => dest.InGameId, src => src.request.InGameId)
                .Map(dest => dest.InGameEventType, src => src.request.InGameEventType)
                .Map(dest => dest.Description, src => src.request.Description)
                .Map(dest => dest.Price, src => src.request.Price)
                .MapToConstructor(true);

            config.NewConfig<CreateInGameEventRequest, CreateInGameEventCommand>()
                .Map(dest => dest.GameServerId, src => src.GameServerId)
                .Map(dest => dest.InGameId, src => src.InGameId)
                .Map(dest => dest.InGameEventType, src => src.InGameEventType)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .MapToConstructor(true);
        }

    }
}
