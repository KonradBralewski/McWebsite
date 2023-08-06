using FluentAssertions;
using McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand;
using McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEvent.Enums;
using McWebsite.Domain.InGameEventOrder;

namespace McWebsite.Application.UnitTests.TestUtils.Extensions.InGameEventOrderExtensions
{
    public static class InGameEventOrderValidationExtensions
    {
        public static void ValidateIfCreatedFrom(this InGameEventOrder inGameEventOrder, CreateInGameEventOrderCommand command)
        {
            inGameEventOrder.Id.Value.ToString().Should().NotBeEmpty();

            inGameEventOrder.BuyingUserId.Value.Should().Be(command.BuyingUserId);

            inGameEventOrder.BoughtInGameEventId.Value.Should().Be(command.BoughtInGameEventId);
        }

        public static void ValidateIfUpdatedFrom(this InGameEventOrder inGameEventOrder, UpdateInGameEventOrderCommand command)
        {
            inGameEventOrder.Id.Value.Should().Be(command.InGameEventOrderId);

            inGameEventOrder.BoughtInGameEventId.Value.Should().Be(command.BoughtInGameEventId);
        }
    }
}
