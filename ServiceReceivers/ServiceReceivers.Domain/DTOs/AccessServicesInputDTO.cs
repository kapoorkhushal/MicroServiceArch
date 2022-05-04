using System;
using System.Collections.Generic;

namespace ServiceReceivers.Domain.DTOs
{
    public class AccessServicesInputDTO
    {
        public Guid UserId { get; set; }
        public List<Guid> ServiceIds { get; set; }
    }
}
