namespace ServiceProviders.Domain.DTOs
{
    public class GetUserByIdOutputDTO
    {
        public string name { get; set; }
        public string mobileNumber { get; set; }
        public string address { get; set; }
        public int pincode { get; set; }
    }
}
