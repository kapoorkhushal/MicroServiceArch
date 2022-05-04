using MediatR;
using ServiceReceivers.Application.Interfaces;
using ServiceReceivers.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceReceivers.Application.Features
{
    public class UserAccessServices : IRequest<int>
    {
        public Guid UserId { get; set; }
        public List<Guid> ServiceIds { get; set; }

        public class AccessServicesHandler : IRequestHandler<UserAccessServices, int>
        {
            private readonly IUserDataService _userServices;
            private readonly IProviderDataService _providerService;
            private readonly IGetServiceAmountDataService _amountService;

            public AccessServicesHandler(IUserDataService userServices, 
                IProviderDataService providerService,
                IGetServiceAmountDataService amountService)
            {
                _userServices = userServices;
                _providerService = providerService;
                _amountService = amountService;
            }
            public async Task<int> Handle(UserAccessServices input, CancellationToken cancellationToken)
            {
                var user = _userServices.GetUserDetails(input.UserId);

                if(user == null)
                {
                    throw new Exception("requested user not found");
                }

                var totalAmount = 0;

                foreach(var serviceId in input.ServiceIds)
                {
                    var accessServiceinput = new AccessServiceProvidersDTO
                    {
                        Pincode = user.Pincode,
                        ServiceId = serviceId,
                        UserId = user.Id
                    };

                    await _providerService.AccessService(accessServiceinput).ConfigureAwait(false);

                    totalAmount += await _amountService.GetServiceAmount(serviceId.ToString(), cancellationToken).ConfigureAwait(false);
                }

                return totalAmount;
            }
        }
    }
}
