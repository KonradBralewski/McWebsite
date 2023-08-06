using FluentAssertions;
using McWebsite.Application.UnitTests.TestEnvironments;
using Moq;
using Xunit;
using McWebsite.Application.GameServerReports.Commands.DeleteGameServerReportCommand;
using McWebsite.Domain.GameServerReport;
using McWebsite.Application.UnitTests.GameServersReports.TestUtils;

namespace McWebsite.Application.UnitTests.GameServersReports.Commands.DeleteGameServerReport
{
    public sealed class DeleteGameServerReportCommandHandlerTests
    {
        private readonly DeleteGameServerReportCommandHandler _handler;
        private readonly UnitTestEnvironments.GameServerReportTestEnvironment _testEnvironment;
        public DeleteGameServerReportCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerReportTestEnvironment.Create();
            _handler = new DeleteGameServerReportCommandHandler(_testEnvironment.MockGameServerReportRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdDeleteGameServerReportCommands))]
        public async Task HandleDeleteGameServerReportCommand_ValidIdCommandGiven_ShouldDeleteGameServerReportAndReturnTrue(DeleteGameServerReportCommand command)
        {
            // Arrange
            var validator = new DeleteGameServerReportCommandValidator();
            int existingGameServersReportsCount = _testEnvironment.GameServersReports.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.Should().BeTrue();
            commandResult.IsError.Should().BeFalse();
            _testEnvironment.MockGameServerReportRepository.Verify(x => x.DeleteGameServerReport(It.IsAny<GameServerReport>()), Times.Once);
            _testEnvironment.GameServersReports.Count().Should().Be(existingGameServersReportsCount - 1);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingIdDeleteGameServerReportCommands))]
        public async Task HandleDeleteGameServerReportCommand_InvalidNotExistingIdCommandGiven_ShouldReturnNotFoundError(DeleteGameServerReportCommand command)
        {
            // Arrange
            var validator = new DeleteGameServerReportCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockGameServerReportRepository.Verify(x => x.DeleteGameServerReport(It.IsAny<GameServerReport>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidIdDeleteGameServerReportCommands()
        {
            var testEnvironment = UnitTestEnvironments.GameServerReportTestEnvironment.Create();
            foreach (GameServerReport gs in testEnvironment.GameServersReports)
            {
                yield return new[] { DeleteGameServerReportCommandUtils.Create(gs.Id.Value) };
            }
        }
        public static IEnumerable<object[]> InvalidNotExistingIdDeleteGameServerReportCommands()
        {
            yield return new[] { DeleteGameServerReportCommandUtils.Create(Guid.NewGuid()) };
            yield return new[] { DeleteGameServerReportCommandUtils.Create(Guid.NewGuid()) };
        }
    }
}
