namespace Module.Core.Entities
{
    public class Zone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Relación con restaurantes ubicados en la zona
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        // Relación muchos a muchos con riders a través de RiderZone
        public virtual ICollection<RiderZone> RiderZones { get; set; } = new List<RiderZone>();
    }
}
