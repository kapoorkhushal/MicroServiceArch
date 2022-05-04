using System;

namespace ServiceProviders.Domain.DTOs
{
    public class EngageServiceProviderInputDTO
    {
        public Guid ServiceProviderId { get; set; }
        public Guid UserId { get; set; }
    }
}
