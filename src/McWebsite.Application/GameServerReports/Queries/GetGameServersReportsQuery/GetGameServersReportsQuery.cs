using ErrorOr;
using MediatR;

namespace McWebsite.Application.GameServerReports.Queries.GetGameServersReportsQuery
{
    public sealed record GetGameServersReportsQuery(int Page, int EntriesPerPage) : IRequest<ErrorOr<GetGameServersReportsResult>>;
}
