using ErrorOr;
using McWebsite.Domain.InGameEventOrder;
using McWebsite.Domain.InGameEventOrder.ValueObjects;

namespace McWebsite.Application.Common.Interfaces.Persistence
{
    public interface IInGameEventOrderRepository
    {
        Task<ErrorOr<IEnumerable<InGameEventOrder>>> GetInGameEventOrders(int page, int entriesPerPage);
        Task<ErrorOr<InGameEventOrder>> GetInGameEventOrder(InGameEventOrderId inGameEventOrderId);
        Task<ErrorOr<InGameEventOrder>> CreateInGameEventOrder(InGameEventOrder inGameEventOrder);
        Task<ErrorOr<InGameEventOrder>> UpdateInGameEventOrder(InGameEventOrder inGameEventOrder);
        Task DeleteInGameEventOrder(InGameEventOrder inGameEventOrder);
    }
}
