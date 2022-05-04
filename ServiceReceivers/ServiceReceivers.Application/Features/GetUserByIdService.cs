using MediatR;
using ServiceReceivers.Application.Interfaces;
using ServiceReceivers.Domain.DTOs;
using ServiceReceivers.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceReceivers.Application.Features
{
    public class GetUserByIdService : IRequest<GetUserByIdOutputDTO>
    {
        public Guid Id { get; set; }

        public class GetUserByIdHandler : IRequestHandler<GetUserByIdService, GetUserByIdOutputDTO>
        {
            private readonly IUserDataService _services;
            public GetUserByIdHandler(IUserDataService context)
            {
                _services = context;
            }
            public async Task<GetUserByIdOutputDTO> Handle(GetUserByIdService query, CancellationToken cancellationToken)
            {
                var user = _services.GetUserDetails(query.Id);
                if (user == null) return null;

                return new GetUserByIdOutputDTO
                {
                    Name = user.Name,
                    Address = user.Address,
                    MobileNumber = user.MobileNumber,
                    Pincode = user.Pincode
                };
            }
        }
    }
}
