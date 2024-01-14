using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User newUser, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(newUser, cancellationToken);
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }

        public async Task<User?> GetByIdAsync(Guid userId, bool includeRoles, bool getSoftDeleted, CancellationToken cancellationToken)
        {
            var query = _dbContext.Users.AsQueryable();
            query = query.Where(u => u.Id.Equals(userId) && (getSoftDeleted || !u.IsDeleted));

            if (includeRoles)
            {
                query = query.Include(u => u.Roles);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, bool includeRoles, bool getSoftDeleted, CancellationToken cancellationToken)
        {
            var query = _dbContext.Users.AsQueryable();
            query = query.Where(u => u.Email == email.ToLower() && (getSoftDeleted || !u.IsDeleted));

            if (includeRoles)
            {
                query = query.Include(u => u.Roles);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
