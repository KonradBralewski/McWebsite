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
        public int MaximumPlayersNumber { get; }
        public int CurrentPlayersNumber { get; }
        public GameServerLocation ServerLocation { get; }
        public GameServerType ServerType { get; }
        public string Description { get; }
        public DateTime UpdatedDateTime { get; }
        private GameServer(GameServerId id,
                           int maximumPlayersNumber,
                           int currentPlayersNumber,
                           GameServerLocation location,
                           GameServerType type,
                           string description,
                           DateTime updatedDateTime) : base(id)
        {
            MaximumPlayersNumber = maximumPlayersNumber;
            CurrentPlayersNumber = currentPlayersNumber;
            ServerLocation = location;
            ServerType = type;
            Description = description;
            UpdatedDateTime = updatedDateTime;
        }

        public static GameServer Create(int maximumPlayersNumber,
                                        int currentPlayersNumber,
                                        ServerLocation location,
                                        ServerType type,
                                        string description,
                                        DateTime updatedDateTime)
        {
            return new GameServer(GameServerId.CreateUnique(),
                maximumPlayersNumber,
                currentPlayersNumber,
                GameServerLocation.Create(location),
                GameServerType.Create(type),
                description,
                updatedDateTime);
        }
    }
}
