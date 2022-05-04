using MediatR;
using ServiceProviders.Application.Interfaces;
using ServiceProviders.Domain.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceProviders.Application.Features
{
    public class EngageServiceProviderStatus : IRequest<GetUserByIdOutputDTO>
    {
        public Guid ServiceProviderId { get; set; }
        public Guid UserId { get; set; }


        public class EngageServiceProviderStatusHandler : IRequestHandler<EngageServiceProviderStatus, GetUserByIdOutputDTO>
        {
            private readonly IProvidersDataService _services;
            private readonly IGetUserDetailsDataService _userService;
            public EngageServiceProviderStatusHandler(IProvidersDataService context, IGetUserDetailsDataService userService)
            {
                _services = context;
                _userService = userService;
            }
            public async Task<GetUserByIdOutputDTO> Handle(EngageServiceProviderStatus provider, CancellationToken cancellationToken)
            {
                var serviceProvider = _services.GetAll().FirstOrDefault(x => x.Id == provider.ServiceProviderId);

                if(serviceProvider == null)
                {
                    throw new Exception("Service Provider not found");
                }

                if(_services.GetAll().Any(x => x.ActiveUserId == provider.UserId))
                {
                    throw new Exception("A Service Provider is already assigned to the user");
                }

                serviceProvider.ActiveUserId = provider.UserId;
                serviceProvider.IsAvailable = false;

                return await _userService.GetUserDetails(provider.UserId.ToString(), cancellationToken).ConfigureAwait(false);

            }
        }
    }
}
