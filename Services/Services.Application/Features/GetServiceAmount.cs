using MediatR;
using Services.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Application.Features
{
    public class GetServiceAmount : IRequest<int>
    {
        public Guid ServiceId { get; set; }

        public class GetServiceAmountHandler : IRequestHandler<GetServiceAmount, int>
        {
            private readonly IServicesDataService _services;
            public GetServiceAmountHandler(IServicesDataService context)
            {
                _services = context;
            }
            public async Task<int> Handle(GetServiceAmount serviceInput, CancellationToken cancellationToken)
            {
                var service = _services.GetAll().Find(x => x.Id == serviceInput.ServiceId);
                if (service == null)
                {
                    throw new Exception("Invalid service");
                }
                return service.Amount;
            }
        }
    }
}
