using ecommerce.Domain.Aggregates.OrderRepository;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore.Storage;

namespace ecommerce.Application.UnitofWorks
{
    /// <summary>
    /// Manages every db contexts and their repositories
    /// </summary>
    public interface IUnitofWork : IDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
        Task<bool> RollbackAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
    }
}
