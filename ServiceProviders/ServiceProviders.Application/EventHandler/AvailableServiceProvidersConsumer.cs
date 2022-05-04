using AutoMapper;
using MassTransit;
using MediatR;
using ServiceProviders.Domain.DTOs;
using ServiceReceivers.Domain.DTOs;
using System;
using System.Threading.Tasks;

namespace ServiceProviders.Application.EventHandler
{
    public class AvailableServiceProvidersConsumer : IConsumer<AccessServiceProvidersDTO>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public AvailableServiceProvidersConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<AccessServiceProvidersDTO> context)
        {
            var provider = new AccessServiceProvider
            {
                Pincode = context.Message.Pincode,
                UserId = context.Message.UserId,
                ServiceId = context.Message.ServiceId
            };
            //var command = _mapper.Map<AccessServiceProvider>(context.Message);
            _ = await _mediator.Send(provider);
        }
    }
}
