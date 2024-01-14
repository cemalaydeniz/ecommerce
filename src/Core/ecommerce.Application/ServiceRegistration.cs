using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ecommerce.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(_ => _.RegisterServicesFromAssemblies(currentAssembly));
            services.AddAutoMapper(currentAssembly);
        }
    }
}
