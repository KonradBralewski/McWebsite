using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServer
{
    public sealed class GameServer : AggregateRoot<GameServerId>
    {
        public GameServerId Id { get; }
        public int MaximumPlayersNumber { get; }
        public GameServerLocation ServerLocation { get; }
        public GameServerType ServerType { get; }
        public string Description { get; }
        private GameServer(GameServerId id,
                           int maximumPlayersNumber,
                           GameServerLocation location,
                           GameServerType type,
                           string description) : base(id)
        {
            Id = id;
            MaximumPlayersNumber = maximumPlayersNumber;
            ServerLocation = location;
            ServerType = type;
            Description = description;
             
        }

        public static GameServer Create(int maximumPlayersNumber,
                                        ServerLocation location,
                                        ServerType type,
                                        string description)
        {
            return new GameServer(GameServerId.CreateUnique(),
                maximumPlayersNumber,
                GameServerLocation.Create(location),
                GameServerType.Create(type),
                description);
        }
    }
}
