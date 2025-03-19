namespace Module.Core.Entities
{
    public class UserActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public UserAction Action { get; set; }
        public EntityType EntityType { get; set; }
        public Guid EntityId { get; set; } // ID del objeto afectado
        public string Changes { get; set; } // JSON con los cambios (antes/después)
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public enum UserAction
    {
        Created,
        Updated,
        Deleted,
        LoggedIn,
        LoggedOut,
        ChangedPassword
    }

    public enum EntityType
    {
        Restaurant,
        Rider,
        SupportUser,
        Order,
        Product
    }
}
