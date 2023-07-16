using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand
{
    public sealed record UpdateInGameEventCommand(Guid InGameEventId,
                                                  Guid GameServerId,
                                                  int InGameId,
                                                  string InGameEventType,
                                                  string Description,
                                                  float Price) : IRequest<ErrorOr<UpdateInGameEventResult>?>;
}
