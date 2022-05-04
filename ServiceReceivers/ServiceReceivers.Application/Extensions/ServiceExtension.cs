using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceReceivers.Application.Features;
using System.Reflection;

namespace ServiceReceivers.Application.Extensions
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<CreateUserService>();
            services.AddScoped<GetUserByIdService>();
            services.AddScoped<UserAccessServices>();
        }
    }
}
