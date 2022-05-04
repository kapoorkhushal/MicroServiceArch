using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ServiceReceivers.Domain.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceReceivers.Webapi.ServiceDiscovery
{
    public class HostedServiceDiscovery : IHostedService
    {
        private readonly IOptions<ServiceConfiguration> _configuration;
        private string registrationId;
        private readonly IConsulClient consulClient;
        public HostedServiceDiscovery(IOptions<ServiceConfiguration> configuration)
        {
            _configuration = configuration;

            consulClient = new ConsulClient(x => {
                x.Address = new Uri(_configuration.Value.ServiceDiscoveryAddress);
            });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var serviceName = _configuration.Value.ServiceName;
            var serviceId = _configuration.Value.ServiceId;
            var serviceAddress = _configuration.Value.ServiceAddress;
            registrationId = $"{serviceName}_{serviceId}";

            var serviceAgent = new AgentServiceRegistration
            {
                ID = registrationId,
                Name = serviceName,
                Address = new Uri(serviceAddress).Host,
                Port = new Uri(serviceAddress).Port
            };

            await consulClient.Agent.ServiceRegister(serviceAgent, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await consulClient.Agent.ServiceDeregister(registrationId);
        }
    }
}
