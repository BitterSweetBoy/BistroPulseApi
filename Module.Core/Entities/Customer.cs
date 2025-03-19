    
namespace Module.Core.Entities
{
    public class Customer : User
    {
        public bool IsCompany { get; set; }
        public bool IsStudent { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();
    }
}
