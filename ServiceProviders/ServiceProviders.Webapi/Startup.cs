using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using ServiceProviders.Application.EventHandler;
using ServiceProviders.Application.Extensions;
using ServiceProviders.Application.Interfaces;
using ServiceProviders.Domain.Configuration;
using ServiceProviders.Domain.DataServiceConfiguration;
using ServiceProviders.Infrastructure.DataService;
using ServiceProviders.Infrastructure.Registry;
using ServiceProviders.Webapi.ServiceDiscovery;
using System;
using System.Net.Http;
using static ServiceProviders.Domain.Constants.EventBusConstants;

namespace ServiceProviders.Webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHostedService, HostedServiceDiscovery>();

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Service Provider Microservice",
                });

            });
            #endregion
            #region Api Versioning
            // Add API Versioning to the Project
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
            #endregion

            services.AddControllers();
            services.AddMassTransit(config => {

                config.AddConsumer<AvailableServiceProvidersConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBus:HostAddress"]);
                    cfg.UseHealthCheck(ctx);

                    cfg.ReceiveEndpoint(AvailableServiceProviders, c => {
                        c.ConfigureConsumer<AvailableServiceProvidersConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
            services.AddApplicationServices();
            services.RegisterService();
            AddReusableHttpClients(services, Configuration);
            services.AddScoped<IServiceRequestConfiguration, ServiceRequestConfiguration>();
            services.AddScoped<IGetUserDetailsDataService, GetUserDetailsDataService>();
            services.Configure<ServiceConfiguration>(Configuration.GetSection("ServiceConfiguration"));
            services.Configure<DataServices>(Configuration.GetSection("DataServices"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Microservice");
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Adds one HttpClient for each data access class which is set in the class default ctor.
        /// The client-baseAddress is set here so that all data service class methods only add the endpoint as a relativeUrl. 
        /// The base and endpoint urls in appsettings.json need to be set accordingly.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private void AddReusableHttpClients(IServiceCollection services, IConfiguration configuration)
        {
            var dataServices = configuration.GetSection("DataServices").Get<DataServices>();

            foreach (var serviceSetting in dataServices.ServiceSettings)
            {
                var serviceName = serviceSetting.Key;
                var settings = serviceSetting.Value;
                services
                .AddHttpClient(serviceName, (client) => client.BaseAddress = settings.BaseUrl)
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetCircuitBreakerPolicy());
            }
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(60));
        }
    }
}
