using FluentAssertions;
using McWebsite.Application.InGameEventOrders.Commands.DeleteInGameEventOrderCommand;
using McWebsite.Application.UnitTests.InGameEventOrders.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.InGameEventOrder;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.InGameEventOrders.Commands.DeleteInGameEventOrder
{
    public sealed class DeleteInGameEventOrderCommandHandlerTests
    {
        private readonly DeleteInGameEventOrderCommandHandler _handler;
        private readonly UnitTestEnvironments.InGameEventOrderTestEnvironment _testEnvironment;
        public DeleteInGameEventOrderCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.InGameEventOrderTestEnvironment.Create();
            _handler = new DeleteInGameEventOrderCommandHandler(_testEnvironment.MockInGameEventOrderRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdDeleteInGameEventOrderCommands))]
        public async Task HandleDeleteInGameEventOrderCommand_ValidIdCommandGiven_ShouldDeleteInGameEventOrderAndReturnTrue(DeleteInGameEventOrderCommand command)
        {
            // Arrange
            var validator = new DeleteInGameEventOrderCommandValidator();
            int existingInGameEventOrdersCount = _testEnvironment.InGameEventOrders.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.Should().BeTrue();
            commandResult.IsError.Should().BeFalse();
            _testEnvironment.MockInGameEventOrderRepository.Verify(x => x.DeleteInGameEventOrder(It.IsAny<InGameEventOrder>()), Times.Once);
            _testEnvironment.InGameEventOrders.Count().Should().Be(existingInGameEventOrdersCount-1);
        }

        [Theory]
        [MemberData(nameof(InvalidIdDeleteInGameEventOrderCommands))]
        public async Task HandleDeleteInGameEventOrderCommand_InvalidIdCommandGiven_ShouldReturnNotFoundError(DeleteInGameEventOrderCommand command)
        {
            // Arrange
            var validator = new DeleteInGameEventOrderCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockInGameEventOrderRepository.Verify(x => x.DeleteInGameEventOrder(It.IsAny<InGameEventOrder>()), Times.Never);
        }
       
        public static IEnumerable<object[]> ValidIdDeleteInGameEventOrderCommands()
        {
            var testEnvironment = UnitTestEnvironments.InGameEventOrderTestEnvironment.Create();

            foreach (InGameEventOrder gs in testEnvironment.InGameEventOrders)
            {
                yield return new[] { DeleteInGameEventOrderCommandUtils.Create(gs.Id.Value) };
            }
        }
        public static IEnumerable<object[]> InvalidIdDeleteInGameEventOrderCommands()
        {
            yield return new[] { DeleteInGameEventOrderCommandUtils.Create(Guid.NewGuid()) };
            yield return new[] { DeleteInGameEventOrderCommandUtils.Create(Guid.NewGuid()) };
        }
    }
}
