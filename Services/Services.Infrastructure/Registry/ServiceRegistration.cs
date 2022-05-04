using Microsoft.Extensions.DependencyInjection;
using Services.Application.Interfaces;
using Services.Infrastructure.DataServices;

namespace Services.Infrastructure.Registry
{
    public static class ServiceRegistration
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IServicesDataService, ServicesDataService>();
        }
    }
}
