using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Common.DomainBase
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
        where TId : notnull
    {
        public TId Id { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public Entity(TId id)
        {
            Id = id;
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }


        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }


        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }
        
        public override int GetHashCode() { return Id.GetHashCode(); }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);
        public static bool operator !=(Entity<TId> left, Entity<TId> right) => !Equals(left, right);

#pragma warning disable CS8618 
        /// <summary>
        /// Parameterless empty constructor for EF Core. (EF Core treats DDD owned entities as regular entites and can't create navigation property)
        /// </summary>
        protected Entity()
        {

        }
#pragma warning restore CS8618

    }
}
