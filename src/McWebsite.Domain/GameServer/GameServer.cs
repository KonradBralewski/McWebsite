using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.Conversation.ValueObjects;
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
    public sealed class GameServer : AggregateRoot<GameServerId, Guid>
    {
        public int MaximumPlayersNumber { get; private set; }
        public int CurrentPlayersNumber { get; private set; }
        public GameServerLocation ServerLocation { get; private set; }
        public GameServerType ServerType { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }
        private GameServer(GameServerId id,
                           int maximumPlayersNumber,
                           int currentPlayersNumber,
                           GameServerLocation location,
                           GameServerType type,
                           string description,
                           DateTime createdDateTime,
                           DateTime updatedDateTime) : base(id)
        {
            MaximumPlayersNumber = maximumPlayersNumber;
            CurrentPlayersNumber = currentPlayersNumber;
            ServerLocation = location;
            ServerType = type;
            Description = description;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static GameServer Create(int maximumPlayersNumber,
                                        int currentPlayersNumber,
                                        ServerLocation location,
                                        ServerType type,
                                        string description,
                                        DateTime createdDateTime,
                                        DateTime updatedDateTime)
        {
            return new GameServer(GameServerId.CreateUnique(),
                maximumPlayersNumber,
                currentPlayersNumber,
                GameServerLocation.Create(location),
                GameServerType.Create(type),
                description,
                createdDateTime,
                updatedDateTime);
        }

        /// <summary>
        /// Constructor that will be used by EF Core, EF Core is not able to setup navigation property for Tuple<UserId, UserId>
        /// </summary>
#pragma warning disable CS8618
        private GameServer()
        {

        }

#pragma warning restore CS8618
    }
}
