using ErrorOr;
using MediatR;

namespace McWebsite.Application.InGameEvents.Queries.GetInGameEventsQuery
{
    public sealed record GetInGameEventsQuery(int Page, int EntriesPerPage) : IRequest<ErrorOr<GetInGameEventsResult>>;
}
