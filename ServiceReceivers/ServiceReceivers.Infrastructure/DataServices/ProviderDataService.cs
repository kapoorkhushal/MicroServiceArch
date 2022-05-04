using MassTransit;
using ServiceReceivers.Application.Interfaces;
using ServiceReceivers.Domain.DTOs;
using System;
using System.Threading.Tasks;

namespace ServiceReceivers.Infrastructure.DataService
{
    public class ProviderDataService : IProviderDataService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ProviderDataService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        /// <inheritdoc/>
        public async Task<bool> AccessService(AccessServiceProvidersDTO input)
        {
            await _publishEndpoint.Publish<AccessServiceProvidersDTO>(input);
            return true;
        }
    }
}
