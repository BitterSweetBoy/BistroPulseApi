namespace Module.Core.Entities
{
    public class ShippingAddress
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } 
        public virtual Customer Customer { get; set; }
        public string AddressName { get; set; }
        public string AddressLine { get; set; }
        public string AddressDetails { get; set; }
        public string AddressPhoneNumber { get; set; }
        public string City { get; set; }
    }
}
