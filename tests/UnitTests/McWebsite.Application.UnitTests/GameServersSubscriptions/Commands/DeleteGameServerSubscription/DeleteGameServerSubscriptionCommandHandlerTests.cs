using FluentAssertions;
using McWebsite.Application.GameServerSubscriptions.Commands.DeleteGameServerSubscriptionCommand;
using McWebsite.Application.UnitTests.GameServersSubscriptions.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.GameServerSubscription;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServersSubscriptions.Commands.DeleteGameServerSubscription
{
    public sealed class DeleteGameServerSubscriptionCommandHandlerTests
    {
        private readonly DeleteGameServerSubscriptionCommandHandler _handler;
        private readonly UnitTestEnvironments.GameServerSubscriptionTestEnvironment _testEnvironment;
        public DeleteGameServerSubscriptionCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerSubscriptionTestEnvironment.Create();
            _handler = new DeleteGameServerSubscriptionCommandHandler(_testEnvironment.MockGameServerSubscriptionRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdDeleteGameServerSubscriptionCommands))]
        public async Task HandleDeleteGameServerSubscriptionCommand_ValidIdCommandGiven_ShouldDeleteGameServerSubscriptionAndReturnTrue(DeleteGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new DeleteGameServerSubscriptionCommandValidator();
            int existingGameServersSubscriptionsCount = _testEnvironment.GameServersSubscriptions.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.Should().BeTrue();
            commandResult.IsError.Should().BeFalse();
            _testEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.DeleteGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Once);
            _testEnvironment.GameServersSubscriptions.Count().Should().Be(existingGameServersSubscriptionsCount - 1);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingIdDeleteGameServerSubscriptionCommands))]
        public async Task HandleDeleteGameServerSubscriptionCommand_InvalidNotExistingIdCommandGiven_ShouldReturnNotFoundError(DeleteGameServerSubscriptionCommand command)
        {
            // Arrange
            var validator = new DeleteGameServerSubscriptionCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.DeleteGameServerSubscription(It.IsAny<GameServerSubscription>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidIdDeleteGameServerSubscriptionCommands()
        {
            var testEnvironment = UnitTestEnvironments.GameServerSubscriptionTestEnvironment.Create();
            foreach (GameServerSubscription gss in testEnvironment.GameServersSubscriptions)
            {
                yield return new[] { DeleteGameServerSubscriptionCommandUtils.Create(gss.Id.Value) };
            }
        }
        public static IEnumerable<object[]> InvalidNotExistingIdDeleteGameServerSubscriptionCommands()
        {
            yield return new[] { DeleteGameServerSubscriptionCommandUtils.Create(Guid.NewGuid()) };
            yield return new[] { DeleteGameServerSubscriptionCommandUtils.Create(Guid.NewGuid()) };
        }
    }
}
