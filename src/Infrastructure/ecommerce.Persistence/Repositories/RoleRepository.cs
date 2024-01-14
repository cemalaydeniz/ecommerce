using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public RoleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Role newRole, CancellationToken cancellationToken)
        {
            await _dbContext.Roles.AddAsync(newRole, cancellationToken);
        }

        public void Update(Role role)
        {
            _dbContext.Roles.Update(role);
        }

        public async Task<Role?> GetByIdAsync(Guid roleId, bool includeUsers, CancellationToken cancellationToken)
        {
            var query = _dbContext.Roles.AsQueryable();
            query = query.Where(r => r.Id.Equals(roleId));

            if (includeUsers)
            {
                query = query.Include(r => r.Users);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Role?> GetByNameAsync(string roleName, bool includeUsers, CancellationToken cancellationToken)
        {
            var query = _dbContext.Roles.AsQueryable();
            query = query.Where(r => r.Name == roleName);

            if (includeUsers)
            {
                query = query.Include(r => r.Users);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Role>> GetRolesofUser(Guid userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Roles
                .Where(r => r.Users.Any(u => u.Id.Equals(userId)))
                .Include(r => r.Users)
                .ToListAsync(cancellationToken);
        }

        public async Task Delete(Guid roleId, CancellationToken cancellationToken)
        {
            Role? role = await GetByIdAsync(roleId, true, cancellationToken);
            if (role == null)
                return;

            role.RemoveFromAllUsers();
            _dbContext.Roles.Remove(role);
        }
    }
}
