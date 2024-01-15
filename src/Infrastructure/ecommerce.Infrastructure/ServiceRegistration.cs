using ecommerce.Application.Services;
using ecommerce.Infrastructure.Stripe;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IStripeService, StripeService>();
        }
    }
}
