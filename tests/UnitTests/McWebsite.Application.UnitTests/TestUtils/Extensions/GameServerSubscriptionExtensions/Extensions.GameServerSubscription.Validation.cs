using FluentAssertions;
using McWebsite.Application.GameServerSubscriptions.Commands.CreateGameServerSubscriptionCommand;
using McWebsite.Application.GameServerSubscriptions.Commands.UpdateGameServerSubscriptionCommand;
using McWebsite.Domain.GameServerSubscription;

namespace McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerSubscriptionExtensions
{
    public static class GameServerSubscriptionValidationExtensions
    {
        public static void ValidateIfCreatedFrom(this GameServerSubscription gameServerSubscription, CreateGameServerSubscriptionCommand command)
        {
            gameServerSubscription.Id.Value.ToString().Should().NotBeEmpty();

            gameServerSubscription.GameServerId.Value.Should().Be(command.GameServerId);

            gameServerSubscription.SubscriptionType.Value.ToString().Should().Be(command.SubscriptionType);

            gameServerSubscription.InGameSubscriptionId.Should().Be(command.InGameSubscriptionId);

            gameServerSubscription.Price.Should().Be(command.Price);

            gameServerSubscription.SubscriptionDescription.Should().Be(command.SubscriptionDescription);

            gameServerSubscription.SubscriptionDuration.Should().Be(command.SubscriptionDuration);
        }

        public static void ValidateIfUpdatedFrom(this GameServerSubscription gameServerSubscription, UpdateGameServerSubscriptionCommand command)
        {
            gameServerSubscription.Id.Value.Should().Be(command.GameServerSubscriptionId);

            gameServerSubscription.GameServerId.Value.Should().Be(command.GameServerId);

            gameServerSubscription.SubscriptionType.Value.ToString().Should().Be(command.SubscriptionType);

            gameServerSubscription.InGameSubscriptionId.Should().Be(command.InGameSubscriptionId);

            gameServerSubscription.Price.Should().Be(command.Price);

            gameServerSubscription.SubscriptionDescription.Should().Be(command.SubscriptionDescription);

            gameServerSubscription.SubscriptionDuration.Should().Be(command.SubscriptionDuration);
        }
    }
}
