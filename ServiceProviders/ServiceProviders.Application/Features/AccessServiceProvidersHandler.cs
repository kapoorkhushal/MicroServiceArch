using MediatR;
using ServiceProviders.Application.Interfaces;
using ServiceProviders.Domain.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceProviders.Application.Features
{
    public class AccessServiceProvidersHandler : IRequestHandler<AccessServiceProvider, string>
    {
        private readonly IProvidersDataService _services;
        public AccessServiceProvidersHandler(IProvidersDataService context)
        {
            _services = context;
        }
        public async Task<string> Handle(AccessServiceProvider providers, CancellationToken cancellationToken)
        {
            var providersList = _services.GetAll();
            if (providersList == null)
            {
                return null;
            }

            var serviceProviders = providersList.FindAll(x => (
                x.IsAvailable == true
                && x.ServiceId == providers.ServiceId
                && x.Pincodes.Exists(x => x == providers.Pincode)
            ));

            if(serviceProviders.Count == 0)
            {
                throw new Exception("no service provider available");
            }

            // send notification to all the service providers until any of them accepts
            while (serviceProviders.Any(x => x.ActiveUserId != providers.UserId))
            {
                foreach (var provider in serviceProviders.AsReadOnly())
                {
                    Console.WriteLine($"notification sent to {provider.Name}");
                }

                await Task.Delay(100000, cancellationToken);
            }

            // return the name of the assigned service provider
            return serviceProviders.Find(x => x.ActiveUserId == providers.UserId).Name;

        }
    }
}
