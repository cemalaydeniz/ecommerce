using MediatR;

namespace ecommerce.Domain.SeedWork
{
    /// <summary>
    /// Declares a class/record as a domain event
    /// </summary>
    public interface IDomainEvent : INotification
    {
    }
}
