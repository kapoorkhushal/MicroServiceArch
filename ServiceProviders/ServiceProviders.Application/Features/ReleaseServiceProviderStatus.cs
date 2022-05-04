using MediatR;
using ServiceProviders.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceProviders.Application.Features
{
    public class ReleaseServiceProviderStatus : IRequest<bool>
    {
        public Guid ServiceProviderId { get; set; }


        public class ReleaseServiceProviderStatusHandler : IRequestHandler<ReleaseServiceProviderStatus, bool>
        {
            private readonly IProvidersDataService _services;
            public ReleaseServiceProviderStatusHandler(IProvidersDataService context)
            {
                _services = context;
            }
            public async Task<bool> Handle(ReleaseServiceProviderStatus provider, CancellationToken cancellationToken)
            {
                var serviceProvider = _services.GetAll().FirstOrDefault(x => x.Id == provider.ServiceProviderId);

                if (serviceProvider == null)
                {
                    throw new Exception("Service Provider not found");
                }

                serviceProvider.ActiveUserId = null;
                serviceProvider.IsAvailable = true;

                return serviceProvider.IsAvailable.Value;
            }
        }
    }
}
