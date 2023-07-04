using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.InGameEventModel.ValueObjects;
using McWebsite.Domain.InGameEventOrder.ValueObjects;
using McWebsite.Domain.User.ValueObjects;

namespace McWebsite.Domain.InGameEventOrder
{
    public sealed class InGameEventOrder : AggregateRoot<InGameEventOrderId>
    { 
        public UserId BuyingUserId { get; private set; }
        public InGameEventId BoughtInGameEventId { get; private set; }

        public DateTime OrderDate { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private InGameEventOrder(InGameEventOrderId id,
                                 UserId buyingUserId,
                                 InGameEventId boughtInGameEventId,
                                 DateTime orderDate,
                                 DateTime updatedDateTime) : base(id)
        {
                BuyingUserId = buyingUserId;
                BoughtInGameEventId = boughtInGameEventId;
                OrderDate = orderDate;
                UpdatedDateTime = updatedDateTime;
        }

        public static InGameEventOrder Create(Guid buyingUserId,
                                              Guid boughtInGameEventId,
                                              DateTime orderDate,
                                              DateTime updatedDateTime)
        {
            return new InGameEventOrder(InGameEventOrderId.CreateUnique(),
                UserId.Create(buyingUserId),
                InGameEventId.Create(boughtInGameEventId),
                orderDate,
                updatedDateTime);
        }
    }
}
