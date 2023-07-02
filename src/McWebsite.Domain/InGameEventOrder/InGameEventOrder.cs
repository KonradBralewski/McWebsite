using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.InGameEventModel.ValueObjects;
using McWebsite.Domain.InGameEventOrder.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.InGameEventOrder
{
    public sealed class InGameEventOrder : AggregateRoot<InGameEventOrderId>
    { 
        public UserId BuyingUserId { get; }
        public InGameEventId BoughtInGameEventId { get; }

        private InGameEventOrder(InGameEventOrderId id,
                                 UserId buyingUserId,
                                 InGameEventId boughtInGameEventId) : base(id)
        {
                BuyingUserId = buyingUserId;
                BoughtInGameEventId = boughtInGameEventId;
        }

        public static InGameEventOrder Create( Guid buyingUserId, Guid boughtInGameEventId)
        {
            return new InGameEventOrder(InGameEventOrderId.CreateUnique(),
                UserId.Recreate(buyingUserId),
                InGameEventId.Recreate(boughtInGameEventId));
        }
    }
}
