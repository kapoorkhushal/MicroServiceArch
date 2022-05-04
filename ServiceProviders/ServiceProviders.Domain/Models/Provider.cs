using System;
using System.Collections.Generic;

namespace ServiceProviders.Domain.Models
{
    public class Provider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsAvailable { get; set; }
        public Guid ServiceId { get; set; }
        public List<int> Pincodes { get; set; }
        public Guid? ActiveUserId { get; set; }
    }
}
