namespace Module.Core.Entities
{
    public class Rider : User
    {
        public string BirthPlace { get; set; }
        public string DNI { get; set; }
        public string Address { get; set; }
        public bool Attendance { get; set; }
        public bool IsAvailable { get; set; }
        // Relación muchos a muchos con zonas, a través de RiderZone
        public virtual ICollection<RiderZone> RiderZones { get; set; } = new List<RiderZone>();
        // Historial de asistencia
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    }
}
