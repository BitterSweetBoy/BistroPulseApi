namespace Module.Core.Entities
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public virtual User User { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; } 
        public string Notes { get; set; } 
    }
}
