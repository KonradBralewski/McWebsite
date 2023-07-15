using FluentAssertions;
using McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerExtensions;
using Moq;
using Xunit;
using McWebsite.Domain.GameServerReport;
using McWebsite.Application.UnitTests.GameServersReports.TestUtils;
using McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerReportExtensions;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.GameServer;

namespace McWebsite.Application.UnitTests.GameServersReports.Commands.UpdateGameServerReport
{
    public sealed class UpdateGameServerReportCommandHandlerTests
    {
        private readonly UpdateGameServerReportCommandHandler _handler;
        private readonly UnitTestEnvironments.GameServerReportTestEnvironment _gameServerReportTestEnvironment;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _gameServerTestEnvironment;
        public UpdateGameServerReportCommandHandlerTests()
        {
  
            _gameServerReportTestEnvironment = UnitTestEnvironments.GameServerReportTestEnvironment.Create();
            _gameServerTestEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();

            _handler = new UpdateGameServerReportCommandHandler(_gameServerReportTestEnvironment.MockGameServerReportRepository.Object,
                                                                _gameServerTestEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidUpdateGameServerReportCommands))]
        public async Task HandleUpdateGameServerReportCommand_ValidCommandGiven_ShouldReturnAndUpdateGameServer(UpdateGameServerReportCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerReportCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeFalse();
            var gameServerReport = commandResult.Value.Value.GameServerReport; // Nullable object (ErrorOr<UpdateGameServerResult>?)
            gameServerReport.Should().NotBeNull();
            gameServerReport.ValidateIfUpdatedFrom(command);
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.UpdateGameServerReport(It.IsAny<GameServerReport>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ValidButWithNoChangesUpdateGameServerReportCommands))]
        public async Task HandleUpdateGameServerReportCommand_ValidCommandGivenButNoChangeIsNeeded_ShouldReturnNull(UpdateGameServerReportCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerReportCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Should().BeNull();
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.UpdateGameServerReport(It.IsAny<GameServerReport>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(NotExistingIdUpdateGameServerReportCommands))]
        public async Task HandleUpdateGameServerReportCommand_NotExistingIdCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerReportCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerReportCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeTrue();
            commandResult.Value.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.UpdateGameServerReport(It.IsAny<GameServerReport>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(NotExistingGameServerIdUpdateGameServerReportCommands))]
        public async Task HandleUpdateGameServerReportCommand_NotExistingGameServerIdCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerReportCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerReportCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeTrue();
            commandResult.Value.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.UpdateGameServerReport(It.IsAny<GameServerReport>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidReportTypeUpdateGameServerReportCommands))]
        public async Task HandleUpdateGameServerReportCommand_InvalidServerTypeCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerReportCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerReportCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.UpdateGameServerReport(It.IsAny<GameServerReport>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEmptyDescriptionCreateGameServerCommands))]
        public async Task HandleUpdateGameServerReportCommand_InvalidEmptyDescriptionCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerReportCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerReportCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.UpdateGameServerReport(It.IsAny<GameServerReport>()), Times.Never);
        }
        public static IEnumerable<object[]> ValidUpdateGameServerReportCommands()
        {
            var testEnvironment = UnitTestEnvironments.GameServerReportTestEnvironment.Create();

            yield return new[] { UpdateGameServerReportCommandUtils.Create(testEnvironment.GameServersReports[0].Id.Value,
                                                                     testEnvironment.GameServersReports[0].GameServerId.Value,
                                                                     "DataLoss",
                                                                     "New description 1") };

            yield return new[] { UpdateGameServerReportCommandUtils.Create(testEnvironment.GameServersReports[1].Id.Value,
                                                                     testEnvironment.GameServersReports[1].GameServerId.Value,
                                                                     "DataLoss",
                                                                     "New description 2") };
        }

        public static IEnumerable<object[]> ValidButWithNoChangesUpdateGameServerReportCommands()
        {
            var testGameServerReportEnvironment = UnitTestEnvironments.GameServerReportTestEnvironment.Create();
            GameServerReport firstGameServerReport = testGameServerReportEnvironment.GameServersReports.ElementAt(0);
            GameServerReport secondGameServerReport = testGameServerReportEnvironment.GameServersReports.ElementAt(1);


            yield return new[] { UpdateGameServerReportCommandUtils.Create(firstGameServerReport.Id.Value,
                                                                           firstGameServerReport.GameServerId.Value,
                                                                           firstGameServerReport.ReportType.Value.ToString(),
                                                                           firstGameServerReport.ReportDescription) };

            yield return new[] { UpdateGameServerReportCommandUtils.Create(secondGameServerReport.Id.Value,
                                                                           secondGameServerReport.GameServerId.Value,
                                                                           secondGameServerReport.ReportType.Value.ToString(),
                                                                           secondGameServerReport.ReportDescription) };
        }
        public static IEnumerable<object[]> NotExistingIdUpdateGameServerReportCommands()
        {
            yield return new[] { UpdateGameServerReportCommandUtils.Create(Guid.NewGuid()) };

            yield return new[] { UpdateGameServerReportCommandUtils.Create(Guid.NewGuid()) };
        }

        public static IEnumerable<object[]> NotExistingGameServerIdUpdateGameServerReportCommands()
        {
            yield return new[] { UpdateGameServerReportCommandUtils.Create(reportedGameServerId: Guid.NewGuid()) };

            yield return new[] { UpdateGameServerReportCommandUtils.Create(reportedGameServerId: Guid.NewGuid()) };
        }

        public static IEnumerable<object[]> InvalidReportTypeUpdateGameServerReportCommands()
        {
            yield return new[] { UpdateGameServerReportCommandUtils.Create(reportType: "badBehavior") };
            yield return new[] { UpdateGameServerReportCommandUtils.Create(reportType: "goodBehavior") };
        }
        public static IEnumerable<object[]> InvalidEmptyDescriptionCreateGameServerCommands()
        {
            yield return new[] { UpdateGameServerReportCommandUtils.Create(description: "") };
            yield return new[] { UpdateGameServerReportCommandUtils.Create(description: "") };
        }
    }
}
