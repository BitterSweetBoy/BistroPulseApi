namespace Module.Core.Entities
{
    public class SupportUser : User
    {
        public string SupportSpecialty { get; set; }
        public bool IsAvailable { get; set; }
        // Historial de asistencia
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    }
}
