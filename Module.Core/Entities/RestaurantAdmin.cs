namespace Module.Core.Entities
{
    public class RestaurantAdmin : User
    {
        public int RestaurantId { get; set; }
        public string DNI { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
