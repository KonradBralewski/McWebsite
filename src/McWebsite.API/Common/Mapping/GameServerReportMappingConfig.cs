using Mapster;
using McWebsite.API.Contracts.GameServer;
using McWebsite.API.Contracts.GameServerReport;
using McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand;
using McWebsite.Application.GameServerReports.Commands.DeleteGameServerReportCommand;
using McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand;
using McWebsite.Application.GameServerReports.Queries.GetGameServerReportQuery;
using McWebsite.Application.GameServerReports.Queries.GetGameServersReportsQuery;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Application.GameServers.Queries.GetGameServerQuery;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using McWebsite.Domain.GameServerReport;

namespace McWebsite.API.Common.Mapping
{
    public class GameServerReportMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            RegisterQueriesCommandsMapping(config);

            config.NewConfig<GameServerReport, GetGameServerReportResponse>()
                .ConstructUsing(src => new GetGameServerReportResponse(src.Id.Value,
                                                                       src.GameServerId.Value,
                                                                       src.ReportType.Value.ToString(),
                                                                       src.ReportDescription,
                                                                       src.ReportDate));

            config.NewConfig<GetGameServersReportsResult, GetGameServersReportsResponse>()
                .Map(dest => dest, src => src.GameServersReports.Select(gsr => gsr.Adapt<GetGameServerReportResponse>()))
                .MapToConstructor(true);

            config.NewConfig<GetGameServerReportResult, GetGameServerReportResponse>()
                .ConstructUsing(src => src.Adapt<GetGameServerReportResponse>());

            config.NewConfig<CreateGameServerReportResult, CreateGameServerReportResponse>()
                .ConstructUsing(src => new CreateGameServerReportResponse(src.GameServerReport.Id.Value,
                                                                           src.GameServerReport.GameServerId.Value,
                                                                           src.GameServerReport.ReportType.Value.ToString(),
                                                                           src.GameServerReport.ReportDescription,
                                                                           src.GameServerReport.ReportDate));
            config.NewConfig<UpdateGameServerReportResult, UpdateGameServerReportResponse>()
                .ConstructUsing(src => new UpdateGameServerReportResponse(src.GameServerReport.Id.Value,
                                                                           src.GameServerReport.GameServerId.Value,
                                                                           src.GameServerReport.ReportType.Value.ToString(),
                                                                           src.GameServerReport.ReportDescription,
                                                                           src.GameServerReport.ReportDate));
        }

        public void RegisterQueriesCommandsMapping(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, GetGameServerReportQuery>()
                .Map(dest => dest.GameServerReportId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(int page, int entriesPerPage), GetGameServersReportsQuery>()
             .Map(dest => dest.Page, src => src.page)
             .Map(dest => dest.EntriesPerPage, src => src.entriesPerPage)
             .MapToConstructor(true);

            config.NewConfig<Guid, DeleteGameServerReportCommand>()
                .Map(dest => dest.GameServerReportId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(Guid gameServerReportId, UpdateGameServerReportRequest request), UpdateGameServerReportCommand>()
                .Map(dest => dest.GameServerReportId, src => src.gameServerReportId)
                .Map(dest => dest.ReportedGameServerId, src => src.request.ReportedGameServerId)
                .Map(dest => dest.ReportType, src => src.request.ReportType)
                .Map(dest => dest.ReportDescription, src => src.request.ReportDescription)
                .MapToConstructor(true);

            config.NewConfig<(Guid reportingUserId, CreateGameServerReportRequest request), CreateGameServerReportCommand>()
             .Map(dest => dest.ReportingUserId, src => src.reportingUserId)
             .Map(dest => dest.ReportedGameServerId, src => src.request.ReportedGameServerId)
             .Map(dest => dest.ReportType, src => src.request.ReportType)
             .Map(dest => dest.ReportDescription, src => src.request.ReportDescription)
             .MapToConstructor(true);
        }

    }
}
