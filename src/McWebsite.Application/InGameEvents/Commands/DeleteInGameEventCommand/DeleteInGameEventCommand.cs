using ErrorOr;
using MediatR;

namespace McWebsite.Application.InGameEvents.Commands.DeleteInGameEventCommand
{
    public sealed record DeleteInGameEventCommand(Guid InGameEventId) : IRequest<ErrorOr<bool>>;
}
