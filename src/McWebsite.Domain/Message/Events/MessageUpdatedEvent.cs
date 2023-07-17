using McWebsite.Domain.Common.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Message.Events
{
    public sealed record MessageUpdatedEvent() : IDomainEvent;
}
