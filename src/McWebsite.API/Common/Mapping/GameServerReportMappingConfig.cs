using Mapster;
using McWebsite.API.Contracts.GameServer;
using McWebsite.API.Contracts.GameServerReport;
using McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand;
using McWebsite.Application.GameServerReports.Queries.GetGameServerReportQuery;
using McWebsite.Application.GameServerReports.Queries.GetGameServersReportsQuery;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Queries.GetGameServerQuery;
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
            config.NewConfig<GetGameServerResult, GetGameServerResponse>()
                .ConstructUsing(src => src.Adapt<GetGameServerResponse>());

            config.NewConfig<GetGameServersReportsResult, GetGameServersReportsResponse>()
                .Map(dest => dest, src => src.GameServersReports.Select(gsr => gsr.Adapt<GetGameServerReportResponse>()));

            config.NewConfig<GetGameServerReportResult, GetGameServerReportResponse>()
                .Map(dest => dest.Id, src => src.GameServerReport.Id.Value)
                .Map(dest => dest.ReportedGameServerId, src => src.GameServerReport.GameServerId.Value)
                .Map(dest => dest.ReportType, src => src.GameServerReport.ReportType.Value.ToString())
                .Map(dest => dest.ReportDescription, src => src.GameServerReport.ReportDescription)
                .Map(dest => dest.ReportDate, src => src.GameServerReport.ReportDate);

            config.NewConfig<(Guid reportingUserId, CreateGameServerReportRequest request), CreateGameServerReportCommand>()
                .Map(dest => dest.ReportingUserId, src => src.reportingUserId)
                .Map(dest => dest.ReportType, src => src.request.ReportType)
                .Map(dest => dest.ReportDescription, src => src.request.ReportDescription);
        }

        public void RegisterQueriesCommandsMapping(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, GetGameServerReportQuery>()
                .Map(dest => dest.GameServerReportId, src => src);
        }

    }
}
