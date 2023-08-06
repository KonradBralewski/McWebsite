using FluentAssertions;
using McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerExtensions;
using Moq;
using Xunit;
using McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerReportExtensions;
using McWebsite.Domain.GameServerReport;
using McWebsite.Application.UnitTests.GameServersReports.TestUtils;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand;

namespace McWebsite.Application.UnitTests.GameServersReports.Commands.CreateGameServerReport
{
    public sealed class CreateGameServerReportCommandHandlerTests
    {
        private readonly CreateGameServerReportCommandHandler _handler;
        private readonly UnitTestEnvironments.GameServerReportTestEnvironment _gameServerReportTestEnvironment;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _gameServerTestEnvironment;
        public CreateGameServerReportCommandHandlerTests()
        {

            _gameServerReportTestEnvironment = UnitTestEnvironments.GameServerReportTestEnvironment.Create();
            _gameServerTestEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();

            _handler = new CreateGameServerReportCommandHandler(_gameServerReportTestEnvironment.MockGameServerReportRepository.Object,
                                                                _gameServerTestEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidCreateGameServerReportCommands))]
        public async Task HandleCreateGameServerReportCommand_ValidCommandGiven_ShouldReturnAndCreateGameServerReport(CreateGameServerReportCommand command)
        {
            // Arrange
            var validator = new CreateGameServerReportCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.GameServerReport.Should().NotBeNull();
            commandResult.IsError.Should().BeFalse();
            commandResult.Value.GameServerReport.ValidateIfCreatedFrom(command);
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.CreateGameServerReport(It.IsAny<GameServerReport>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingGameServerIdCreateGameServerReportCommands))]
        public async Task HandleCreateGameServerReportCommand_InvalidNotExistingGameServerIdCommandGiven_ShouldReturnNotFoundError(CreateGameServerReportCommand command)
        {
            // Arrange
            var validator = new CreateGameServerReportCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.CreateGameServerReport(It.IsAny<GameServerReport>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidReportTypeCreateGameServerReportCommands))]
        public async Task HandleCreateGameServerReportCommand_InvalidReportTypeCommandGiven_ShouldBeCatchedByValidator(CreateGameServerReportCommand command)
        {
            // Arrange
            var validator = new CreateGameServerReportCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.CreateGameServerReport(It.IsAny<GameServerReport>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEmptyDescriptionCreateGameServerReportCommands))]
        public async Task HandleCreateGameServerReportCommand_InvalidEmptyDescriptionCommandGiven_ShouldBeCatchedByValidator(CreateGameServerReportCommand command)
        {
            // Arrange
            var validator = new CreateGameServerReportCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _gameServerReportTestEnvironment.MockGameServerReportRepository.Verify(x => x.CreateGameServerReport(It.IsAny<GameServerReport>()), Times.Never);
        }
        public static IEnumerable<object[]> ValidCreateGameServerReportCommands()
        {
            yield return new[] { CreateGameServerReportCommandUtils.Create() };
            yield return new[] { CreateGameServerReportCommandUtils.Create(reportType: "DataLoss", description: "Test description 123") };
            yield return new[] { CreateGameServerReportCommandUtils.Create(reportType: "ExploitableVulnerability", description: "Test description 321") };
        }

        public static IEnumerable<object[]> InvalidNotExistingGameServerIdCreateGameServerReportCommands()
        {
            yield return new[] { CreateGameServerReportCommandUtils.Create(reportedGameServerId : Guid.NewGuid())};
            yield return new[] { CreateGameServerReportCommandUtils.Create(reportedGameServerId: Guid.NewGuid())};
            yield return new[] { CreateGameServerReportCommandUtils.Create(reportedGameServerId: Guid.NewGuid()) };
        }
        public static IEnumerable<object[]> InvalidReportTypeCreateGameServerReportCommands()
        {
            yield return new[] { CreateGameServerReportCommandUtils.Create(reportType: "BadManagment", description: "Test description 1234") };
            yield return new[] { CreateGameServerReportCommandUtils.Create(reportType: "HighPrices", description: "Test description 12345") };
            yield return new[] { CreateGameServerReportCommandUtils.Create(reportType: "BoringGameplay", description: "Test description 123456") };
        }

        public static IEnumerable<object[]> InvalidEmptyDescriptionCreateGameServerReportCommands()
        {
            yield return new[] { CreateGameServerReportCommandUtils.Create(description: "") };
            yield return new[] { CreateGameServerReportCommandUtils.Create(description: "") };
            yield return new[] { CreateGameServerReportCommandUtils.Create(description: "") };
        }
    }
}
