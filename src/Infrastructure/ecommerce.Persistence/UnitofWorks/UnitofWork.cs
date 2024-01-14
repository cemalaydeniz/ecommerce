using ecommerce.Application.UnitofWorks;
using ecommerce.Domain.Aggregates.OrderRepository;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Persistence.Context;
using ecommerce.Persistence.Exceptions;
using ecommerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ecommerce.Persistence.UnitofWorks
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _dbContext;

        private IDbContextTransaction? _dbContextTransaction;

        public UnitofWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            _dbContextTransaction = null;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContextTransaction != null)
                throw new TransactionAlreadyBeganException("A transaction for AppDbContext has already begun");

            _dbContextTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            return _dbContextTransaction;
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContextTransaction == null)
                throw new TransactionNotStartedException();

            bool anyChanges = await SaveChangesAsync(cancellationToken);
            await _dbContextTransaction.CommitAsync(cancellationToken);
            return anyChanges;
        }

        public async Task<bool> RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContextTransaction == null)
                throw new TransactionNotStartedException();

            bool anyChanges = await SaveChangesAsync(cancellationToken);
            await _dbContextTransaction.RollbackAsync(cancellationToken);
            return anyChanges;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            _dbContextTransaction?.Dispose();
            _dbContextTransaction = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region Repositories
        private IUserRepository? _userRepository = null;
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);

        private IRoleRepository? _roleRepository = null;
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_dbContext);

        private IProductRepository? _productRepository = null;
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_dbContext);

        private IOrderRepository? _orderRepository = null;
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_dbContext);
        #endregion
    }
}
