using ErrorOr;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using McWebsite.Domain.GameServerSubscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Common.Interfaces.Persistence
{
    public interface IGameServerSubscriptionRepository
    {
        Task<ErrorOr<IEnumerable<GameServerSubscription>>> GetGameServersSubscriptions(int page, int entriesPerPage);
        Task<ErrorOr<GameServerSubscription>> GetGameServerSubscription(GameServerSubscriptionId gameServerSubscriptionId);
        Task<ErrorOr<GameServerSubscription>> CreateGameServerSubscription(GameServerSubscription gameServerSubscription);
        Task<ErrorOr<GameServerSubscription>> UpdateGameServerSubscription(GameServerSubscription gameServerSubscription);
        Task DeleteGameServerSubscription(GameServerSubscription gameServerSubscription);
    }
}
