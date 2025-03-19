namespace Module.Core.Entities
{
    public class RiderZone
    {
        public string RiderId { get; set; }
        public virtual Rider Rider { get; set; }

        public int ZoneId { get; set; }
        public virtual Zone Zone { get; set; }
    }
}
