using Mapster;
using McWebsite.API.Contracts.GameServerSubscription;
using McWebsite.Application.GameServerSubscriptions.Commands.CreateGameServerSubscriptionCommand;
using McWebsite.Application.GameServerSubscriptions.Commands.DeleteGameServerSubscriptionCommand;
using McWebsite.Application.GameServerSubscriptions.Commands.UpdateGameServerSubscriptionCommand;
using McWebsite.Application.GameServerSubscriptions.Queries.GetGameServersSubscriptionsQuery;
using McWebsite.Application.GameServerSubscriptions.Queries.GetGameServerSubscriptionQuery;
using McWebsite.Domain.GameServerSubscription;

namespace McWebsite.API.Common.Mapping
{
    public class GameServerSubscriptionMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            RegisterQueriesCommandsMapping(config);

            config.NewConfig<GameServerSubscription, GetGameServerSubscriptionResponse>()
                .ConstructUsing(src => new GetGameServerSubscriptionResponse(src.Id.Value,
                                                                       src.GameServerId.Value,
                                                                       src.SubscriptionType.Value.ToString(),
                                                                       src.InGameSubscriptionId,
                                                                       src.Price,
                                                                       src.SubscriptionDescription,
                                                                       src.SubscriptionDuration,
                                                                       src.CreatedDateTime));

            config.NewConfig<GetGameServersSubscriptionsResult, GetGameServersSubscriptionsResponse>()
                .Map(dest => dest, src => src.GameServersSubscriptions.Select(gsr => gsr.Adapt<GetGameServerSubscriptionResponse>()))
                .MapToConstructor(true);

            config.NewConfig<GetGameServerSubscriptionResult, GetGameServerSubscriptionResponse>()
                .ConstructUsing(src => src.GameServerSubscription.Adapt<GetGameServerSubscriptionResponse>());

            config.NewConfig<CreateGameServerSubscriptionResult, CreateGameServerSubscriptionResponse>()
                .ConstructUsing(src => new CreateGameServerSubscriptionResponse(src.GameServerSubscription.Id.Value,
                                                                                src.GameServerSubscription.GameServerId.Value,
                                                                                src.GameServerSubscription.SubscriptionType.Value.ToString(),
                                                                                src.GameServerSubscription.InGameSubscriptionId,
                                                                                src.GameServerSubscription.Price,
                                                                                src.GameServerSubscription.SubscriptionDescription,
                                                                                src.GameServerSubscription.SubscriptionDuration,
                                                                                src.GameServerSubscription.CreatedDateTime));

            config.NewConfig<UpdateGameServerSubscriptionResult, UpdateGameServerSubscriptionResponse>()
                .ConstructUsing(src => new UpdateGameServerSubscriptionResponse(src.GameServerSubscription.Id.Value,
                                                                                src.GameServerSubscription.GameServerId.Value,
                                                                                src.GameServerSubscription.SubscriptionType.Value.ToString(),
                                                                                src.GameServerSubscription.InGameSubscriptionId,
                                                                                src.GameServerSubscription.Price,
                                                                                src.GameServerSubscription.SubscriptionDescription,
                                                                                src.GameServerSubscription.SubscriptionDuration,
                                                                                src.GameServerSubscription.CreatedDateTime));
        }

        public void RegisterQueriesCommandsMapping(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, GetGameServerSubscriptionQuery>()
                .Map(dest => dest.GameServerSubscriptionId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(int page, int entriesPerPage), GetGameServersSubscriptionsQuery>()
             .Map(dest => dest.Page, src => src.page)
             .Map(dest => dest.EntriesPerPage, src => src.entriesPerPage)
             .MapToConstructor(true);

            config.NewConfig<Guid, DeleteGameServerSubscriptionCommand>()
                .Map(dest => dest.GameServerSubscriptionId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(Guid gameServerSubscriptionId, UpdateGameServerSubscriptionRequest request), UpdateGameServerSubscriptionCommand>()
                .Map(dest => dest.GameServerSubscriptionId, src => src.gameServerSubscriptionId)
                .Map(dest => dest.GameServerId, src => src.request.GameServerId)
                .Map(dest => dest.SubscriptionType, src => src.request.SubscriptionType)
                .Map(dest => dest.InGameSubscriptionId, src => src.request.InGameSubscriptionId)
                .Map(dest => dest.Price, src => src.request.Price)
                .Map(dest => dest.SubscriptionDescription, src => src.request.SubscriptionDescription)
                .Map(dest => dest.SubscriptionDuration, src => src.request.SubscriptionDuration)
                .MapToConstructor(true);

            config.NewConfig<(Guid reportingUserId, CreateGameServerSubscriptionRequest request), CreateGameServerSubscriptionCommand>()
                .Map(dest => dest.GameServerId, src => src.request.GameServerId)
                .Map(dest => dest.SubscriptionType, src => src.request.SubscriptionType)
                .Map(dest => dest.InGameSubscriptionId, src => src.request.InGameSubscriptionId)
                .Map(dest => dest.Price, src => src.request.Price)
                .Map(dest => dest.SubscriptionDescription, src => src.request.SubscriptionDescription)
                .Map(dest => dest.SubscriptionDuration, src => src.request.SubscriptionDuration)
                .MapToConstructor(true);
        }

    }
}
