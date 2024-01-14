namespace ecommerce.Domain.Aggregates.RoleAggregate
{
    /// <summary>
    /// Available CRUD operations for the aggregate root: <see cref="Role"/>
    /// </summary>
    public interface IRoleRepository
    {
        Task AddAsync(Role newRole, CancellationToken cancellationToken = default);
        void Update(Role role);
        Task<Role?> GetByIdAsync(Guid roleId, bool includeUsers, CancellationToken cancellationToken = default);
        Task<Role?> GetByNameAsync(string roleName, bool includeUsers, CancellationToken cancellationToken = default);
        Task<List<Role>> GetRolesofUser(Guid userId, CancellationToken cancellationToken = default);
        Task Delete(Guid roleId, CancellationToken cancellationToken = default);
    }
}
