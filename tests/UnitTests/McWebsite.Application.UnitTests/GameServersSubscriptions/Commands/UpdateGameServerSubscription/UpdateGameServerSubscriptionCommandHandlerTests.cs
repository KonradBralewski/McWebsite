using FluentAssertions;
using McWebsite.Application.GameServerSubscriptions.Commands.UpdateGameServerSubscriptionCommand;
using McWebsite.Application.UnitTests.GameServersSubscriptions.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerSubscriptionExtensions;
using McWebsite.Domain.GameServerSubscription;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServersSubscriptions.Commands.UpdateGameServerSubscription
{
    public sealed class UpdateGameServerSubscriptionCommandHandlerTests
    {
        private readonly UpdateGameServerSubscriptionCommandHandler _handler;
        private readonly UnitTestEnvironments.GameServerSubscriptionTestEnvironment _gameServerSubscriptionTestEnvironment;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _gameServerTestEnvironment;
        public UpdateGameServerSubscriptionCommandHandlerTests()
        {
  
            _gameServerSubscriptionTestEnvironment = UnitTestEnvironments.GameServerSubscriptionTestEnvironment.Create();
            _gameServerTestEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();

            _handler = new UpdateGameServerSubscriptionCommandHandler(_gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Object,
                                                                      _gameServerTestEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidUpdateGameServerSubscriptionCommands))]
        public async Task HandleUpdateGameServerSubscriptionCommand_ValidCommandGiven_ShouldReturnAndUpdateGameServerSubscription(UpdateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerSubscriptionCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeFalse();
            var gameServerSubscription = commandResult.Value.Value.GameServerSubscription; // Nullable object (ErrorOr<UpdateGameServerResult>?)
            gameServerSubscription.Should().NotBeNull();
            gameServerSubscription.ValidateIfUpdatedFrom(command);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ValidButWithNoChangesUpdateGameServerSubscriptionCommands))]
        public async Task HandleUpdateGameServerSubscriptionCommand_ValidCommandGivenButNoChangeIsNeeded_ShouldReturnNull(UpdateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerSubscriptionCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Should().BeNull();
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(NotExistingGameServerIdUpdateGameServerSubscriptionCommands))]
        public async Task HandleUpdateGameServerSubscriptionCommand_NotExistingGameServerIdCommandGiven_ShouldReturn404NotFound(UpdateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerSubscriptionCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeTrue();
            commandResult.Value.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidSubscriptionTypeUpdateGameServerSubscriptionCommands))]
        public async Task HandleUpdateGameServerSubscriptionCommand_InvalidSubscriptionTypeCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidInGameSubscriptionIdUpdateGameServerSubscriptionCommands))]
        public async Task HandleUpdateGameServerSubscriptionCommand_InvalidInGameSubscriptionIdCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidPriceUpdateGameServerSubscriptionCommands))]
        public async Task HandleUpdateGameServerSubscriptionCommand_InvalidPriceCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEmptyDescriptionUpdateGameServerSubscriptionCommands))]
        public async Task HandleUpdateGameServerSubscriptionCommand_InvalidEmptyDescriptionCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidSubscriptionDurationUpdateGameServerSubscriptionCommands))]
        public async Task HandleUpdateGameServerSubscriptionCommand_InvalidSubscriptionDurationCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }
        public static IEnumerable<object[]> ValidUpdateGameServerSubscriptionCommands()
        {
            var testEnvironment = UnitTestEnvironments.GameServerSubscriptionTestEnvironment.Create();

            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(testEnvironment.GameServersSubscriptions[0].Id.Value,
                                                                                 testEnvironment.GameServersSubscriptions[0].GameServerId.Value,
                                                                                 "AdventureQuests",
                                                                                 1234440,
                                                                                 5000,
                                                                                 "New description 1",
                                                                                 TimeSpan.FromDays(2)) };

            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(testEnvironment.GameServersSubscriptions[1].Id.Value,
                                                                                 testEnvironment.GameServersSubscriptions[1].GameServerId.Value,
                                                                                 "HardcoreIntense",
                                                                                 1234441,
                                                                                 6000,
                                                                                 "New description 2",
                                                                                 TimeSpan.FromDays(4)) };
        }

        public static IEnumerable<object[]> ValidButWithNoChangesUpdateGameServerSubscriptionCommands()
        {
            var testEnvironment = UnitTestEnvironments.GameServerSubscriptionTestEnvironment.Create();

            foreach(GameServerSubscription gss in testEnvironment.GameServersSubscriptions)
            {
                yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(gss.Id.Value,
                                                                                     gss.GameServerId.Value,
                                                                                     gss.SubscriptionType.Value.ToString(),
                                                                                     gss.InGameSubscriptionId,
                                                                                     gss.Price,
                                                                                     gss.SubscriptionDescription,
                                                                                     gss.SubscriptionDuration) };
            }

        }
        public static IEnumerable<object[]> NotExistingGameServerIdUpdateGameServerSubscriptionCommands()
        {
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(gameServerId: Guid.NewGuid()) };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(gameServerId: Guid.NewGuid()) };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(gameServerId: Guid.NewGuid()) };
        }
        public static IEnumerable<object[]> InvalidSubscriptionTypeUpdateGameServerSubscriptionCommands()
        {
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(subscriptionType: "BadManagment") };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(subscriptionType: "HighPrices") };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(subscriptionType: "BoringGameplay") };
        }

        public static IEnumerable<object[]> InvalidInGameSubscriptionIdUpdateGameServerSubscriptionCommands()
        {
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(inGameSubscriptionId: 0) };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(inGameSubscriptionId: -100) };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(inGameSubscriptionId: -200) };
        }

        public static IEnumerable<object[]> InvalidPriceUpdateGameServerSubscriptionCommands()
        {
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(price: 0) };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(price: -50) };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(price: -500) };
        }

        public static IEnumerable<object[]> InvalidEmptyDescriptionUpdateGameServerSubscriptionCommands()
        {
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(subscriptionDescription: "") };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(subscriptionDescription: "") };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(subscriptionDescription: "") };
        }

        public static IEnumerable<object[]> InvalidSubscriptionDurationUpdateGameServerSubscriptionCommands()
        {
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(subscriptionDuration: TimeSpan.MinValue) };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(subscriptionDuration: TimeSpan.FromSeconds(-30)) };
            yield return new[] { UpdateGameServerSubscriptionCommandUtils.Create(subscriptionDuration: TimeSpan.FromSeconds(-500)) };
        }
    }
}
