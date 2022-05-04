using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceProviders.Application.EventHandler;
using ServiceProviders.Application.Features;
using System.Reflection;

namespace ServiceProviders.Application.Extensions
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<EngageServiceProviderStatus>();
            services.AddScoped<AccessServiceProvidersHandler>();
            services.AddScoped<ReleaseServiceProviderStatus>();
            services.AddScoped<AvailableServiceProvidersConsumer>();
        }
    }
}
