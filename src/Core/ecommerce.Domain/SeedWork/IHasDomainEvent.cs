﻿namespace ecommerce.Domain.SeedWork
{
    /// <summary>
    /// Declares that the entity has domain events
    /// </summary>
    public interface IHasDomainEvent
    {
        void AddDomainEvents(IDomainEvent domainEvent);
        void ClearDomainEvents();

        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    }
}
