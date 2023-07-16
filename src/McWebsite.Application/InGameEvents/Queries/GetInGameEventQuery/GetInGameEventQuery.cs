using ErrorOr;
using MediatR;

namespace McWebsite.Application.InGameEvents.Queries.GetInGameEventQuery
{
    public sealed record GetInGameEventQuery(Guid InGameEventId) : IRequest<ErrorOr<GetInGameEventResult>>;
}
