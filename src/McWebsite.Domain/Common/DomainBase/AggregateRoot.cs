using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Common.DomainBase
{
    public abstract class AggregateRoot<TId> : Entity<TId>
        where TId : notnull
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }

#pragma warning disable CS8618 
        /// <summary>
        /// Parameterless constructor for EF Core. (EF Core treats DDD owned entities as regular entites and can't create navigation property)
        /// </summary>
        protected AggregateRoot()
        {

        }
#pragma warning restore CS8618
    }
}
