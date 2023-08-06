using FluentAssertions;
using McWebsite.Application.GameServerSubscriptions.Commands.CreateGameServerSubscriptionCommand;
using McWebsite.Application.UnitTests.GameServersSubscriptions.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerSubscriptionExtensions;
using McWebsite.Domain.GameServerSubscription;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServersSubscriptions.Commands.CreateGameServerSubscription
{
    public sealed class CreateGameServerSubscriptionCommandHandlerTests
    {
        private readonly CreateGameServerSubscriptionCommandHandler _handler;
        private readonly UnitTestEnvironments.GameServerSubscriptionTestEnvironment _gameServerSubscriptionTestEnvironment;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _gameServerTestEnvironment;
        public CreateGameServerSubscriptionCommandHandlerTests()
        {

            _gameServerSubscriptionTestEnvironment = UnitTestEnvironments.GameServerSubscriptionTestEnvironment.Create();
            _gameServerTestEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();

            _handler = new CreateGameServerSubscriptionCommandHandler(_gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Object,
                                                                      _gameServerTestEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidCreateGameServerSubscriptionCommands))]
        public async Task HandleCreateGameServerSubscriptionCommand_ValidCommandGiven_ShouldReturnAndCreateGameServerSubscription(CreateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new CreateGameServerSubscriptionCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.GameServerSubscription.Should().NotBeNull();
            commandResult.IsError.Should().BeFalse();
            commandResult.Value.GameServerSubscription.ValidateIfCreatedFrom(command);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.CreateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingGameServerIdCreateGameServerSubscriptionCommands))]
        public async Task HandleCreateGameServerSubscriptionCommand_InvalidNotExistingGameServerIdCommandGiven_ShouldReturnNotFoundError(CreateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new CreateGameServerSubscriptionCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidSubscriptionTypeCreateGameServerSubscriptionCommands))]
        public async Task HandleCreateGameServerSubscriptionCommand_InvalidSubscriptionTypeCommandGiven_ShouldBeCatchedByValidator(CreateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new CreateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.CreateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidInGameSubscriptionIdCreateGameServerSubscriptionCommands))]
        public async Task HandleCreateGameServerSubscriptionCommand_InvalidInGameSubscriptionIdCommandGiven_ShouldBeCatchedByValidator(CreateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new CreateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.CreateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidPriceCreateGameServerSubscriptionCommands))]
        public async Task HandleCreateGameServerSubscriptionCommand_InvalidPriceCommandGiven_ShouldBeCatchedByValidator(CreateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new CreateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.CreateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEmptyDescriptionCreateGameServerSubscriptionCommands))]
        public async Task HandleCreateGameServerSubscriptionCommand_InvalidEmptyDescriptionCommandGiven_ShouldBeCatchedByValidator(CreateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new CreateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.CreateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidSubscriptionDurationCreateGameServerSubscriptionCommands))]
        public async Task HandleCreateGameServerSubscriptionCommand_InvalidSubscriptionDurationCommandGiven_ShouldBeCatchedByValidator(CreateGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new CreateGameServerSubscriptionCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerSubscriptionTestEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.CreateGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }
        public static IEnumerable<object[]> ValidCreateGameServerSubscriptionCommands()
        {
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create() };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionType: "ModdedAccess", inGameSubscriptionId: 199984)};
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionType: "MinigamesUnlocked", inGameSubscriptionId: 199985) };
        }

        public static IEnumerable<object[]> InvalidNotExistingGameServerIdCreateGameServerSubscriptionCommands()
        {
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(gameServerId : Guid.NewGuid())};
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(gameServerId: Guid.NewGuid())};
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(gameServerId: Guid.NewGuid()) };
        }
        public static IEnumerable<object[]> InvalidSubscriptionTypeCreateGameServerSubscriptionCommands()
        {
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionType: "BadManagment") };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionType: "HighPrices") };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionType: "BoringGameplay") };
        }

        public static IEnumerable<object[]> InvalidInGameSubscriptionIdCreateGameServerSubscriptionCommands()
        {
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(inGameSubscriptionId: 0) };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(inGameSubscriptionId: -100) };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(inGameSubscriptionId: -200) };
        }

        public static IEnumerable<object[]> InvalidPriceCreateGameServerSubscriptionCommands()
        {
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(price: 0) };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(price: -50) };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(price: -500) };
        }

        public static IEnumerable<object[]> InvalidEmptyDescriptionCreateGameServerSubscriptionCommands()
        {
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionDescription: "") };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionDescription: "") };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionDescription: "") };
        }

        public static IEnumerable<object[]> InvalidSubscriptionDurationCreateGameServerSubscriptionCommands()
        {
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionDuration: TimeSpan.MinValue) };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionDuration: TimeSpan.FromSeconds(-30)) };
            yield return new[] { CreateGameServerSubscriptionCommandUtils.Create(subscriptionDuration: TimeSpan.FromSeconds(-500)) };
        }
    }
}
