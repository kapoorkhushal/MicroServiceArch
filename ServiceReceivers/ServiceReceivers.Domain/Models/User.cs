using System;

namespace ServiceReceivers.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public bool? IsServiceActive { get; set; }

    }
}
