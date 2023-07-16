using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;

namespace McWebsite.API.Contracts.InGameEventOrder
{
    public sealed record CreateInGameEventOrderRequest(Guid BuyingUserId,
                                                       Guid BoughtInGameEventId);
}
