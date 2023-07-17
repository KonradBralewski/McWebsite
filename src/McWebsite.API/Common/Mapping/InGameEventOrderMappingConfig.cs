using Mapster;
using McWebsite.API.Contracts.InGameEventOrder;
using McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand;
using McWebsite.Application.InGameEventOrders.Commands.DeleteInGameEventOrderCommand;
using McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand;
using McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrderQuery;
using McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrdersQuery;
using McWebsite.Domain.InGameEventOrder;

namespace McWebsite.API.Common.Mapping
{
    public class InGameEventOrderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            RegisterQueriesCommandsMapping(config);

            config.NewConfig<InGameEventOrder, GetInGameEventOrderResponse>()
                .ConstructUsing(src => new GetInGameEventOrderResponse(src.Id.Value,
                                                                       src.BuyingUserId.Value,
                                                                       src.BoughtInGameEventId.Value));

            config.NewConfig<GetInGameEventOrdersResult, GetInGameEventOrdersResponse>()
                .Map(dest => dest, src => src.InGameEventOrders.Select(gsr => gsr.Adapt<GetInGameEventOrderResponse>()))
                .MapToConstructor(true);

            config.NewConfig<GetInGameEventOrderResult, GetInGameEventOrderResponse>()
                .ConstructUsing(src => src.InGameEventOrder.Adapt<GetInGameEventOrderResponse>());

            config.NewConfig<CreateInGameEventOrderResult, CreateInGameEventOrderResponse>()
                .ConstructUsing(src => new CreateInGameEventOrderResponse(src.InGameEventOrder.Id.Value,
                                                                       src.InGameEventOrder.BuyingUserId.Value,
                                                                       src.InGameEventOrder.BoughtInGameEventId.Value,
                                                                       src.InGameEventOrder.OrderDate));

            config.NewConfig<UpdateInGameEventOrderResult, UpdateInGameEventOrderResponse>()
                .ConstructUsing(src => new UpdateInGameEventOrderResponse(src.InGameEventOrder.Id.Value,
                                                                          src.InGameEventOrder.BuyingUserId.Value,
                                                                          src.InGameEventOrder.BoughtInGameEventId.Value,
                                                                          src.InGameEventOrder.UpdatedDateTime));
        }

        public void RegisterQueriesCommandsMapping(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, GetInGameEventOrderQuery>()
                .Map(dest => dest.InGameEventOrderId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(int page, int entriesPerPage), GetInGameEventOrdersQuery>()
             .Map(dest => dest.Page, src => src.page)
             .Map(dest => dest.EntriesPerPage, src => src.entriesPerPage)
             .MapToConstructor(true);

            config.NewConfig<Guid, DeleteInGameEventOrderCommand>()
                .Map(dest => dest.InGameEventOrderId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(Guid inGameEventOrderId, UpdateInGameEventOrderRequest request), UpdateInGameEventOrderCommand>()
                .Map(dest => dest.InGameEventOrderId, src => src.inGameEventOrderId)
                .Map(dest => dest.BoughtInGameEventId, src => src.request.BoughtInGameEventId)
                .MapToConstructor(true);

            config.NewConfig<(Guid buyingUserId, CreateInGameEventOrderRequest request), CreateInGameEventOrderCommand>()
                .Map(dest => dest.BuyingUserId, src => src.buyingUserId)
                .Map(dest => dest.BoughtInGameEventId, src => src.request.BoughtInGameEventId)
                .MapToConstructor(true);
        }
    }
}
