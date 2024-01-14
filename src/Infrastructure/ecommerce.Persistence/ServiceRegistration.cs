using ecommerce.Application.Services;
using ecommerce.Application.UnitofWorks;
using ecommerce.Persistence.Authentication;
using ecommerce.Persistence.Context;
using ecommerce.Persistence.Interceptors;
using ecommerce.Persistence.UnitofWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceProvider, IConfiguration configuration)
        {
            serviceProvider.AddDbContext<AppDbContext>(_ => _.UseNpgsql(configuration.GetConnectionString("Default")));
            serviceProvider.AddScoped<PublishDomainEventsInterceptor>();
            serviceProvider.AddScoped<IUnitofWork, UnitofWork>();
            serviceProvider.AddScoped<IJwtTokenService, JwtTokenService>();
        }
    }
}
