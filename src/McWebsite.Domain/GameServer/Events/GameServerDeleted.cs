using McWebsite.Domain.Common.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServer.Events
{
    public sealed record GameServerDeleted() : IDomainEvent; // TODO: Take care of it when GameServerSubscriptions & Reports will be implemented
}
