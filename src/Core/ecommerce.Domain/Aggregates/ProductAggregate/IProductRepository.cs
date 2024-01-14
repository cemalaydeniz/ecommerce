namespace ecommerce.Domain.Aggregates.ProductAggregate
{
    /// <summary>
    /// Available CRUD operations for the aggregate root: <see cref="Product"/>
    /// </summary>
    public interface IProductRepository
    {
        Task AddAsync(Product newProduct, CancellationToken cancellationToken = default);
        void Update(Product product);
        Task<Product?> GetByIdAsync(Guid productId, bool getSoftDeleted, CancellationToken cancellationToken = default);
        Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> productIds, bool getSoftDeleted, CancellationToken cancellationToken = default);
        Task<List<Product>> SearchByNameAsync(string name, int page, int pageSize, bool getSoftDeleted, CancellationToken cancellationToken = default);
    }
}
