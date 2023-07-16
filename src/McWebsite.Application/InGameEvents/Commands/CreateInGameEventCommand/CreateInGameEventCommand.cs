using ErrorOr;
using MediatR;

namespace McWebsite.Application.InGameEvents.Commands.CreateInGameEventCommand
{
    public sealed record CreateInGameEventCommand(Guid GameServerId,
                                                  int InGameId,
                                                  string InGameEventType,
                                                  string Description,
                                                  float Price) : IRequest<ErrorOr<CreateInGameEventResult>>;
}
