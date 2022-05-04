using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Services.Application.Features;
using System.Reflection;

namespace Services.Application.Extensions
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<GetAllServices>();
        }
    }
}
