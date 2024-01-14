#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


namespace ecommerce.Domain.SeedWork
{
    /// <summary>
    /// Base class of the entites that are not aggregate roots
    /// </summary>
    /// <typeparam name="TKey">The type of the Id of the entity</typeparam>
    public class BaseEntity<TKey> : IEquatable<BaseEntity<TKey>>, IHasDomainEvent
        where TKey : notnull
    {
        public TKey Id { get; protected set; }

        public override bool Equals(object? obj)
        {
            return obj is BaseEntity<TKey> other && this.Id.Equals(other.Id);
        }

        public bool Equals(BaseEntity<TKey>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(BaseEntity<TKey>? left, BaseEntity<TKey>? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseEntity<TKey>? left, BaseEntity<TKey>? right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvents(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
