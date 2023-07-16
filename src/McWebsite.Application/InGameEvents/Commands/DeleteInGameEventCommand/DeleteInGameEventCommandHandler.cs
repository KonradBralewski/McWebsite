using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEvent.ValueObjects;
using MediatR;

namespace McWebsite.Application.InGameEvents.Commands.DeleteInGameEventCommand
{
    public sealed class DeleteInGameEventCommandHandler : IRequestHandler<DeleteInGameEventCommand, ErrorOr<bool>>
    {
        private readonly IInGameEventRepository _inGameEventRepository;
        public DeleteInGameEventCommandHandler(IInGameEventRepository inGameEventRepository)
        {
            _inGameEventRepository = inGameEventRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteInGameEventCommand request, CancellationToken cancellationToken)
        {
            var inGameEventSearchResult = await _inGameEventRepository.GetInGameEvent(
                InGameEventId.Create(request.InGameEventId));

            if (inGameEventSearchResult.IsError)
            {
                return inGameEventSearchResult.Errors;
            }

            InGameEvent inGameEvent = inGameEventSearchResult.Value;

            await _inGameEventRepository.DeleteInGameEvent(inGameEvent);

            return true;
        }
    }
}
