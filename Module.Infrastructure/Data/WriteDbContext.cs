using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Module.Core.Entities;


namespace Module.Infrastructure.Data
{
    public class WriteDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }

        // DbSets
        public DbSet<SessionTicket> SessionTickets { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantAdmin> RestaurantAdmins { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<RiderZone> RiderZones { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<SupportUser> SupportUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // **Schema: Auth (Autenticación y sesiones)**
            builder.Entity<User>().ToTable("Usuarios", "Auth");
            builder.Entity<IdentityRole>().ToTable("Roles", "Auth");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UsuarioClaims", "Auth");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsuarioRoles", "Auth");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UsuarioLogins", "Auth");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RolClaims", "Auth");
            builder.Entity<IdentityUserToken<string>>().ToTable("UsuarioTokens", "Auth");
            builder.Entity<SessionTicket>().ToTable("SessionTickets", "Auth");

            // Schema: Payments (Pagos)
            builder.Entity<PaymentTransaction>().ToTable("Transactions", "Payments");
            builder.Entity<PaymentMethod>().ToTable("Methods", "Payments");

            // Schema: Orders (Pedidos y direcciones de envío)
            builder.Entity<ShippingAddress>().ToTable("ShippingAddresses", "Orders");

            // Schema: Restaurant (Restaurantes y administradores)
            builder.Entity<Restaurant>().ToTable("Restaurants", "Restaurant");
            builder.Entity<RestaurantAdmin>().ToTable("Admins", "Restaurant");

            // Schema: Riders (Repartidores y zonas)
            builder.Entity<Rider>().ToTable("Riders", "Riders");
            builder.Entity<RiderZone>().ToTable("RiderZones", "Riders");
            builder.Entity<Zone>().ToTable("Zones", "Riders");

            // Schema: Logs (Registros de actividad y asistencia)
            builder.Entity<UserActivityLog>().ToTable("ActivityLogs", "Logs");
            builder.Entity<AttendanceRecord>().ToTable("AttendanceRecords", "Logs");

            // Schema: Users (Clientes y soporte)
            builder.Entity<Customer>().ToTable("Customers", "Users");
            builder.Entity<SupportUser>().ToTable("SupportUsers", "Users");

            // Configuraciones de relaciones

            // User - SessionTicket (Uno a muchos)
            builder.Entity<SessionTicket>()
               .HasOne(st => st.User)
               .WithMany(u => u.SessionTickets)
               .HasForeignKey(st => st.UserId);

            // Customer - PaymentMethod (Uno a muchos)
            builder.Entity<Customer>()
               .HasOne(c => c.PaymentMethod)
               .WithMany()
               .HasForeignKey(c => c.PaymentMethodId);

            // Customer - ShippingAddresses (Uno a muchos)
            builder.Entity<ShippingAddress>()
               .HasOne(sa => sa.Customer)
               .WithMany(c => c.ShippingAddresses)
               .HasForeignKey(sa => sa.CustomerId);

            // Restaurant - PaymentMethod (Uno a uno)
            builder.Entity<Restaurant>()
               .HasOne(r => r.PaymentMethod)
               .WithMany()
               .HasForeignKey(r => r.PaymentMethodId);

            // Restaurant - Zone (Uno a muchos)
            builder.Entity<Restaurant>()
               .HasOne(r => r.Zone)
               .WithMany(z => z.Restaurants)
               .HasForeignKey(r => r.ZoneId);

            // RestaurantAdmin - Restaurant (Uno a muchos)
            builder.Entity<RestaurantAdmin>()
               .HasOne(ra => ra.Restaurant)
               .WithMany(r => r.Administrators)
               .HasForeignKey(ra => ra.RestaurantId);

            // PaymentTransaction - Customer (Uno a muchos)
            builder.Entity<PaymentTransaction>()
               .HasOne(pt => pt.Customer)
               .WithMany()
               .HasForeignKey(pt => pt.CustomerId);

            // PaymentTransaction - PaymentMethod (Uno a muchos)
            builder.Entity<PaymentTransaction>()
               .HasOne(pt => pt.PaymentMethod)
               .WithMany()
               .HasForeignKey(pt => pt.PaymentMethodId);

            // UserActivityLog - User (Uno a muchos)
            builder.Entity<UserActivityLog>()
               .HasOne(ual => ual.User)
               .WithMany()
               .HasForeignKey(ual => ual.UserId);

            // Rider - RiderZone (Muchos a muchos)
            builder.Entity<RiderZone>()
               .HasKey(rz => new { rz.RiderId, rz.ZoneId });

            // AttendanceRecord - User (Uno a muchos)
            builder.Entity<AttendanceRecord>()
               .HasOne(ar => ar.User)
               .WithMany()
               .HasForeignKey(ar => ar.UserId);

        }
    }
}
