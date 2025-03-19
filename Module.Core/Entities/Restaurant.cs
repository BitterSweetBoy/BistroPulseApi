namespace Module.Core.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessLicenseUrl { get; set; }
        public DateTime Established { get; set; }
        public string WorkingPeriod { get; set; }
        public string Address { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        // Cada restaurante se ubica en una zona
        public int ZoneId { get; set; }
        public virtual Zone Zone { get; set; }
        // Relación con los administradores asignados al restaurante
        public virtual ICollection<RestaurantAdmin> Administrators { get; set; } = new List<RestaurantAdmin>();
    }

}
