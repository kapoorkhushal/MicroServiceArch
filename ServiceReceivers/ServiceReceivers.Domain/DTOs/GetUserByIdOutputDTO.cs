using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceReceivers.Domain.DTOs
{
    public class GetUserByIdOutputDTO
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
    }
}
