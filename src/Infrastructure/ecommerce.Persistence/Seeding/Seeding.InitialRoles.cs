using ecommerce.Application.UnitofWorks;
using ecommerce.Domain.Aggregates.RoleAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Persistence.Seeding
{
    public static partial class Seeding
    {
        public async static Task SeedInitialRoles(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                IUnitofWork unitofWork = scope.ServiceProvider.GetRequiredService<IUnitofWork>();

                var initialRoles = new[]
                {
                    new Role(Application.Utilities.Constants.ConstantsUtility.Role.User),
                    new Role(Application.Utilities.Constants.ConstantsUtility.Role.Admin)
                    // ... more roles can be added here
                };

                foreach (var role in initialRoles)
                {
                    Role? existingRole = await unitofWork.RoleRepository.GetByNameAsync(role.Name, false);
                    if (existingRole != null)
                        continue;

                    await unitofWork.RoleRepository.AddAsync(role);
                }

                await unitofWork.SaveChangesAsync();
            }
        }
    }
}
