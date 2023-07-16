using ErrorOr;
using McWebsite.Domain.InGameEvent.ValueObjects;
using McWebsite.Domain.InGameEvent.Entities;

namespace McWebsite.Application.Common.Interfaces.Persistence
{
    public interface IInGameEventRepository
    {
        Task<ErrorOr<IEnumerable<InGameEvent>>> GetInGameEvents(int page, int entriesPerPage);
        Task<ErrorOr<InGameEvent>> GetInGameEvent(InGameEventId inGameEventId);
        Task<ErrorOr<InGameEvent>> CreateInGameEvent(InGameEvent inGameEvent);
        Task<ErrorOr<InGameEvent>> UpdateInGameEvent(InGameEvent inGameEvent);
        Task DeleteInGameEvent(InGameEvent inGameEvent);
    }
}
