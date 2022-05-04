using MediatR;
using System;

namespace ServiceProviders.Domain.DTOs
{
    public class AccessServiceProvider : IRequest<string>
    {
        public int Pincode { get; set; }
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }

    }
}
