using System;

namespace ServiceReceivers.Domain.DTOs
{
    public class AccessServiceProvidersDTO
    {
        public int Pincode { get; set; }
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }

    }
}
