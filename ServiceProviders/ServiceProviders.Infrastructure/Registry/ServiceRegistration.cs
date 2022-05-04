using Microsoft.Extensions.DependencyInjection;
using ServiceProviders.Application.Interfaces;
using ServiceProviders.Infrastructure.DataService;

namespace ServiceProviders.Infrastructure.Registry
{
    public static class ServiceRegistration
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IProvidersDataService, ProvidersDataService>();
        }
    }
}
