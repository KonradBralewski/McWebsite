using FluentAssertions;
using McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand;
using McWebsite.Application.UnitTests.InGameEventOrders.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.InGameEventOrderExtensions;
using McWebsite.Domain.InGameEventOrder;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.InGameEventOrders.Commands.UpdateInGameEventOrder
{
    public sealed class UpdateInGameEventOrderCommandHandlerTests
    {
        private readonly UpdateInGameEventOrderCommandHandler _handler;
        private readonly UnitTestEnvironments.InGameEventOrderTestEnvironment _inGameEventOrderTestEnvironment;
        private readonly UnitTestEnvironments.InGameEventTestEnvironment _inGameEventTestEnvironment;
        public UpdateInGameEventOrderCommandHandlerTests()
        {
            _inGameEventOrderTestEnvironment = UnitTestEnvironments.InGameEventOrderTestEnvironment.Create();
            _inGameEventTestEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();

            _handler = new UpdateInGameEventOrderCommandHandler(_inGameEventOrderTestEnvironment.MockInGameEventOrderRepository.Object,
                                                                _inGameEventTestEnvironment.MockInGameEventRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidUpdateInGameEventOrderCommands))]
        public async Task HandleUpdateInGameEventOrderCommand_ValidCommandGiven_ShouldReturnAndUpdateInGameEventOrder(UpdateInGameEventOrderCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventOrderCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeFalse();
            var inGameEventOrder = commandResult.Value.Value.InGameEventOrder; // Nullable object (ErrorOr<UpdateInGameEventOrderResult>?)
            inGameEventOrder.Should().NotBeNull();
            inGameEventOrder.ValidateIfUpdatedFrom(command);
            _inGameEventOrderTestEnvironment.MockInGameEventOrderRepository.Verify(x => x.UpdateInGameEventOrder(It.IsAny<InGameEventOrder>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ValidButWithNoChangesUpdateInGameEventOrderCommands))]
        public async Task HandleUpdateInGameEventOrderCommand_ValidCommandGivenButWithNoChangeIsNeeded_ShouldReturnNull(UpdateInGameEventOrderCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventOrderCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Should().BeNull();
            _inGameEventOrderTestEnvironment.MockInGameEventOrderRepository.Verify(x => x.UpdateInGameEventOrder(It.IsAny<InGameEventOrder>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(NotExistingIdUpdateInGameEventOrderCommands))]
        public async Task HandleUpdateInGameEventOrderCommand_NotExistingIdCommandGiven_ShouldBeCatchedByValidator(UpdateInGameEventOrderCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventOrderCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeTrue();
            commandResult.Value.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _inGameEventOrderTestEnvironment.MockInGameEventOrderRepository.Verify(x => x.UpdateInGameEventOrder(It.IsAny<InGameEventOrder>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(NotExistingInGameEventIdUpdateInGameEventOrderCommands))]
        public async Task HandleUpdateInGameEventOrderCommand_InvalidMaximumPlayersNumberCommandGiven_ShouldBeCatchedByValidator(UpdateInGameEventOrderCommand command)
        { // Arrange
            var validator = new UpdateInGameEventOrderCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeTrue();
            commandResult.Value.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _inGameEventOrderTestEnvironment.MockInGameEventOrderRepository.Verify(x => x.UpdateInGameEventOrder(It.IsAny<InGameEventOrder>()), Times.Never);
        }
        public static IEnumerable<object[]> ValidUpdateInGameEventOrderCommands()
        {
            var inGameEventOrderTestEnvironment = UnitTestEnvironments.InGameEventOrderTestEnvironment.Create();
            var inGameEventTestEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();

            // boughtEvent changed to different event (to event bought in second order)

            yield return new[] { UpdateInGameEventOrderCommandUtils.Create(inGameEventOrderTestEnvironment.InGameEventOrders[0].Id.Value,
                                                                           inGameEventTestEnvironment.InGameEvents[1].Id.Value)};
            // boughtEvent changed to different event (to event bought in last order, we do not touch first ordered event as it was changed)
            yield return new[] { UpdateInGameEventOrderCommandUtils.Create(inGameEventOrderTestEnvironment.InGameEventOrders[1].Id.Value,
                                  inGameEventTestEnvironment.InGameEvents[inGameEventTestEnvironment.InGameEvents.Count-1].Id.Value) };
        }

        public static IEnumerable<object[]> ValidButWithNoChangesUpdateInGameEventOrderCommands()
        {
            var testEnvironment = UnitTestEnvironments.InGameEventOrderTestEnvironment.Create();

            foreach(InGameEventOrder igeo in testEnvironment.InGameEventOrders)
            {
                yield return new[] { UpdateInGameEventOrderCommandUtils.Create(igeo.Id.Value,
                                                                               igeo.BoughtInGameEventId.Value) };
            }
           
        }
        public static IEnumerable<object[]> NotExistingIdUpdateInGameEventOrderCommands()
        {
            yield return new[] { UpdateInGameEventOrderCommandUtils.Create(Guid.NewGuid()) };

            yield return new[] { UpdateInGameEventOrderCommandUtils.Create(Guid.NewGuid()) };
        }
        public static IEnumerable<object[]> NotExistingInGameEventIdUpdateInGameEventOrderCommands()
        {
            yield return new[] { UpdateInGameEventOrderCommandUtils.Create(boughtInGameEventId: Guid.NewGuid()) };

            yield return new[] { UpdateInGameEventOrderCommandUtils.Create(boughtInGameEventId: Guid.NewGuid()) };
        }
    }
}
