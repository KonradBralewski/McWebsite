using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.GameServers.TestMocks
{
    public static partial class OurMocks
    {
        public static class MockIGameServerRepository
        {
            public static List<GameServer> GameServers = new List<GameServer>();
            public static Mock<IGameServerRepository> GetMock()
            {
                Mock<IGameServerRepository> mock = new Mock<IGameServerRepository>();

                mock.Setup(m => m.CreateGameServer(It.IsAny<GameServer>()))
                    .Callback((GameServer gameServer) => GameServers.Add(gameServer))
                    .ReturnsAsync((GameServer gameServer) => gameServer);

                mock.Setup(m => m.GetGameServer(It.IsAny<GameServerId>())).ReturnsAsync((GameServerId Id)
                    => GameServers.FirstOrDefault(gs => gs.Id == Id));

                return mock;
            }
        }

    }
}
