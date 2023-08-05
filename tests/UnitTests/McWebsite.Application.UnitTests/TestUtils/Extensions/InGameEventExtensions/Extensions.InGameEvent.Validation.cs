using FluentAssertions;
using McWebsite.Application.InGameEvents.Commands.CreateInGameEventCommand;
using McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEvent.Enums;

namespace McWebsite.Application.UnitTests.TestUtils.Extensions.InGameEventExtensions
{
    public static class InGameEventValidationExtensions
    {
        public static void ValidateIfCreatedFrom(this InGameEvent inGameEvent, CreateInGameEventCommand command)
        {
            inGameEvent.Id.Value.ToString().Should().NotBeEmpty();

            inGameEvent.GameServerId.Value.Should().Be(command.GameServerId);

            inGameEvent.InGameId.Should().Be(command.InGameId);

            inGameEvent.InGameEventType.Value.ToString().Should().BeOneOf(Enum.GetNames(typeof(EventType)));

            inGameEvent.Description.Should().Be(command.Description);

            inGameEvent.Price.Should().Be(command.Price);
        }

        public static void ValidateIfUpdatedFrom(this InGameEvent inGameEvent, UpdateInGameEventCommand command)
        {
            inGameEvent.Id.Value.Should().Be(command.InGameEventId);

            inGameEvent.GameServerId.Value.Should().Be(command.GameServerId);

            inGameEvent.InGameId.Should().Be(command.InGameId);

            inGameEvent.InGameEventType.Value.ToString().Should().BeOneOf(Enum.GetNames(typeof(EventType)));

            inGameEvent.Description.Should().Be(command.Description);

            inGameEvent.Price.Should().Be(command.Price);
        }
    }
}
