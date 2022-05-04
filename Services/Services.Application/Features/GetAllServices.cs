using MediatR;
using Services.Application.Interfaces;
using Services.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Application.Features
{
    public class GetAllServices : IRequest<IEnumerable<Service>>
    {
        public class GetAllServicesHandler : IRequestHandler<GetAllServices, IEnumerable<Service>>
        {
            private readonly IServicesDataService _services;
            public GetAllServicesHandler(IServicesDataService context)
            {
                _services = context;
            }
            public async Task<IEnumerable<Service>> Handle(GetAllServices query, CancellationToken cancellationToken)
            {
                var servicesList = _services.GetAll();
                if (servicesList == null)
                {
                    return null;
                }
                return servicesList.AsReadOnly();
            }
        }
    }
}
