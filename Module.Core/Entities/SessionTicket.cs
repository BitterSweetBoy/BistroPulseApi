namespace Module.Core.Entities
{
    public class SessionTicket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; }
        public string SessionKey { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public bool IsSessionExpired { get; set; } = false;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime? LogoutAt { get; set; }
        public bool RevokedByAdmin { get; set; } = false;
        public string TicketData { get; set; }

        // Propiedad de navegación para la relación
        public virtual User User { get; set; }
    }
}
