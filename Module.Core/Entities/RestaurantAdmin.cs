namespace Module.Core.Entities
{
    public class RestaurantAdmin
    {
        public int RestaurantId { get; set; }
        public string UserId { get; set; } = default!;
        public string DNI { get; set; }
        public Restaurant Restaurant { get; set; }
        public User User { get; set; }
    }
}
