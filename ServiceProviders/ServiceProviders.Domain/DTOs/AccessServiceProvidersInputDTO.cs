using System;

namespace ServiceProviders.Domain.DTOs
{
    public class AccessServiceProvidersInputDTO
    {
        public int Pincode { get; set; }
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
    }
}
