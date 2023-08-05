using FluentAssertions;
using FluentValidation;
using McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand;
using McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand;
using McWebsite.Application.UnitTests.InGameEventOrders.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.InGameEventOrderExtensions;
using McWebsite.Domain.InGameEventOrder;
using Moq;
using Xunit;
using static McWebsite.Application.UnitTests.TestEnvironments.UnitTestEnvironments;

namespace McWebsite.Application.UnitTests.InGameEventOrders.Commands.CreateInGameEventOrder
{
    public sealed class CreateInGameEventOrderCommandHandlerTests
    {
        private readonly CreateInGameEventOrderCommandHandler _handler;
        private readonly UnitTestEnvironments.InGameEventOrderTestEnvironment _inGameEventOrderTestEnvironment;
        private readonly UnitTestEnvironments.InGameEventTestEnvironment _inGameEventTestEnvironment;
        private readonly UnitTestEnvironments.UserTestEnvironment _userTestEnvironment;
        public CreateInGameEventOrderCommandHandlerTests()
        {
            _inGameEventOrderTestEnvironment = UnitTestEnvironments.InGameEventOrderTestEnvironment.Create();
            _inGameEventTestEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();
            _userTestEnvironment = UnitTestEnvironments.UserTestEnvironment.Create();

            _handler = new CreateInGameEventOrderCommandHandler(_inGameEventOrderTestEnvironment.MockInGameEventOrderRepository.Object,
                                                                _inGameEventTestEnvironment.MockInGameEventRepository.Object,
                                                                _userTestEnvironment.MockUserRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidCreateInGameEventOrderCommands))]
        public async Task HandleCreateInGameEventOrderCommand_ValidCommandGiven_ShouldReturnAndCreateInGameEventOrder(CreateInGameEventOrderCommand command)
        {
            // Arrange
            var validator = new CreateInGameEventOrderCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.InGameEventOrder.Should().NotBeNull();
            commandResult.IsError.Should().BeFalse();
            commandResult.Value.InGameEventOrder.ValidateIfCreatedFrom(command);
            _inGameEventOrderTestEnvironment.MockInGameEventOrderRepository.Verify(x => x.CreateInGameEventOrder(It.IsAny<InGameEventOrder>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingUserIdCreateInGameEventOrderCommands))]
        public async Task HandleCreateInGameEventOrderCommand_InvalidNotExistingUserIdCommandGiven_ShouldBeCatchedByValidator(CreateInGameEventOrderCommand command)
        {
            // Arrange
            var validator = new CreateInGameEventOrderCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _inGameEventOrderTestEnvironment.MockInGameEventOrderRepository.Verify(x => x.CreateInGameEventOrder(It.IsAny<InGameEventOrder>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingInGameEventIdCreateInGameEventOrderCommands))]
        public async Task HandleCreateInGameEventOrderCommand_InvalidNotExistingInGameEventIdCommandGiven_ShouldBeCatchedByValidator(CreateInGameEventOrderCommand command)
        {
            // Arrange
            var validator = new CreateInGameEventOrderCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _inGameEventOrderTestEnvironment.MockInGameEventOrderRepository.Verify(x => x.CreateInGameEventOrder(It.IsAny<InGameEventOrder>()), Times.Never);
        }

        public static IEnumerable<object[]>ValidCreateInGameEventOrderCommands()
        {
            var gameEventTestEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();

            yield return new[] { CreateInGameEventOrderCommandUtils.Create() };
            yield return new[] { CreateInGameEventOrderCommandUtils.Create(boughtInGameEventId: gameEventTestEnvironment.InGameEvents[1].Id.Value) };
        }

        public static IEnumerable<object[]> InvalidNotExistingUserIdCreateInGameEventOrderCommands()
        {
            yield return new[] { CreateInGameEventOrderCommandUtils.Create(buyingUserId: Guid.NewGuid()) };
            yield return new[] { CreateInGameEventOrderCommandUtils.Create(buyingUserId: Guid.NewGuid()) };
        }

        public static IEnumerable<object[]> InvalidNotExistingInGameEventIdCreateInGameEventOrderCommands()
        {
            yield return new[] { CreateInGameEventOrderCommandUtils.Create(boughtInGameEventId: Guid.NewGuid()) };
            yield return new[] { CreateInGameEventOrderCommandUtils.Create(boughtInGameEventId: Guid.NewGuid()) };
        }
    }
}
