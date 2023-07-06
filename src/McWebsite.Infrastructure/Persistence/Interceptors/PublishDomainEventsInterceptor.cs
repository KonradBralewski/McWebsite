using McWebsite.Domain.Common.DomainBase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace McWebsite.Infrastructure.Persistence.Interceptors
{
    public sealed class PublishDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IPublisher _publisher;
        public PublishDomainEventsInterceptor(IPublisher publisher)
        {
            _publisher = publisher;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            throw new Exception("Only async call is allowed.");
        }

        public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchEvents(DbContext? dbContext)
        {
            if(dbContext == null)
            {
                return;
            }

            var entitiesWithEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
                .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
                .Select(entityEntry => entityEntry.Entity)
                .ToList();

            IReadOnlyCollection<IDomainEvent> domainEvents = entitiesWithEvents.SelectMany(entity => entity.DomainEvents).ToList();

            foreach(IHasDomainEvents entity in entitiesWithEvents)
            {
                entity.ClearDomainEvents();
            }

            foreach (IDomainEvent domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
