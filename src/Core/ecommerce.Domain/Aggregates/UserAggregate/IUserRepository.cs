namespace ecommerce.Domain.Aggregates.UserAggregate
{
    /// <summary>
    /// Available CRUD operations for the aggregate root: <see cref="User"/>
    /// </summary>
    public interface IUserRepository
    {
        Task AddAsync(User newUser, CancellationToken cancellationToken = default);
        void Update(User user);
        Task<User?> GetByIdAsync(Guid userId, bool includeRoles, bool getSoftDeleted, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, bool includeRoles, bool getSoftDeleted, CancellationToken cancellationToken = default);
    }
}
