using Microsoft.Extensions.DependencyInjection;
using ServiceReceivers.Application.Interfaces;
using ServiceReceivers.Infrastructure.DataService;

namespace ServiceReceivers.Infrastructure.Registry
{
    public static class ServiceRegistration
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IUserDataService, UserDataService>();
        }
    }
}
