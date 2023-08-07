using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServerReport.ValueObjects;
using Moq;

namespace McWebsite.Application.UnitTests.TestEnvironments
{
    public static partial class UnitTestEnvironments
    {
        private static List<GameServerReport> GameServersReports = new List<GameServerReport>
            {
                GameServerReport.Recreate(Constants.GameServerReportQueriesAndCommands.Id,
                                    Constants.GameServerReportQueriesAndCommands.ReportedGameServerId,
                                    Constants.GameServerReportQueriesAndCommands.ReportingUserId,
                                    ReportType.LaggingPerformance,
                                    "TestDescription NUMBER 1",
                                    DateTime.UtcNow,
                                    DateTime.UtcNow),
                GameServerReport.Create(
                                    Constants.GameServerReportQueriesAndCommands.ReportedGameServerId,
                                    Constants.GameServerReportQueriesAndCommands.ReportingUserId,
                                    ReportType.DataLoss,
                                    "TestDescription NUMBER 1",
                                    DateTime.UtcNow,
                                    DateTime.UtcNow)
            };
        public class GameServerReportTestEnvironment
        {
            public List<GameServerReport> GameServersReports = null!;
            public Mock<IGameServerReportRepository> MockGameServerReportRepository = null!;
            private GameServerReportTestEnvironment()
            {
            }

            public static GameServerReportTestEnvironment Create()
            {
                GameServerReportTestEnvironment testEnvironment = new GameServerReportTestEnvironment();

                testEnvironment.GameServersReports = new List<GameServerReport>(UnitTestEnvironments.GameServersReports);
                testEnvironment.MockGameServerReportRepository = GetMock(testEnvironment.GameServersReports);

                return testEnvironment;
            }

            public static Mock<IGameServerReportRepository> GetMock(List<GameServerReport> testCollection)
            {
                Mock<IGameServerReportRepository> mock = new Mock<IGameServerReportRepository>();

                mock.Setup(m => m.CreateGameServerReport(It.IsAny<GameServerReport>()))
                    .ReturnsAsync((GameServerReport gameServerReport) => {
                        testCollection.Add(gameServerReport);
                        return gameServerReport;
                    });

                mock.Setup(m => m.GetGameServerReport(It.IsAny<GameServerReportId>())).ReturnsAsync((GameServerReportId Id)
                    =>
                {
                    if (testCollection.FirstOrDefault(gsr => gsr.Id.Value == Id.Value) is not GameServerReport foundGameServerReport)
                    {
                        return Errors.DomainModels.ModelNotFound;
                    }

                    return foundGameServerReport;
                });

                mock.Setup(m => m.GetGameServersReports(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync((int page, int entriesPerPage) =>
                    testCollection.OrderBy(p => p.ReportDate)
                        .Skip(page)
                        .Take(entriesPerPage)
                        .ToList());

                mock.Setup(m => m.DeleteGameServerReport(It.IsAny<GameServerReport>()))
                    .Returns((GameServerReport gameServerReport) =>
                    {
                        testCollection.RemoveAll(gsrEntry => gsrEntry.Id.Value == gameServerReport.Id.Value);
                        return Task.CompletedTask;
                    });

                mock.Setup(m => m.UpdateGameServerReport(It.IsAny<GameServerReport>()))
                    .ReturnsAsync((GameServerReport updatedGameServerReport) =>
                    {
                        int foundServerReportIndex = testCollection.FindIndex(gsr => gsr.Id.Value == updatedGameServerReport.Id.Value);

                        testCollection[foundServerReportIndex] = GameServerReport.Recreate(updatedGameServerReport.Id.Value,
                                                                         updatedGameServerReport.GameServerId.Value,
                                                                         updatedGameServerReport.ReportingUserId.Value,
                                                                         updatedGameServerReport.ReportType.Value,
                                                                         updatedGameServerReport.ReportDescription,
                                                                         updatedGameServerReport.ReportDate,
                                                                         updatedGameServerReport.UpdatedDateTime);
                        return updatedGameServerReport;
                    });

                return mock;
            }



        }
    }
}
