using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ecommerce.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(_ => _.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
