using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Product newProduct, CancellationToken cancellationToken)
        {
            await _dbContext.Products.AddAsync(newProduct, cancellationToken);
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
        }

        public async Task<Product?> GetByIdAsync(Guid productId, bool getSoftDeleted, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id.Equals(productId) && (getSoftDeleted || !p.IsDeleted), cancellationToken);
        }

        public async Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> productIds, bool getSoftDeleted, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Where(p => (getSoftDeleted || !p.IsDeleted) && productIds.Any(id => id.Equals(p.Id)))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> SearchByNameAsync(string name, int page, int pageSize, bool getSoftDeleted, CancellationToken cancellationToken)
        {
            if (page < 1 || pageSize <= 0)
                return new List<Product>();

            return await _dbContext.Products
                .Where(p => (getSoftDeleted || !p.IsDeleted) && EF.Functions.ILike(p.Name, $"%{name}%"))
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
