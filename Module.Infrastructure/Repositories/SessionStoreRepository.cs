using Microsoft.AspNetCore.Authentication;
using Module.Infrastructure.Data;
using Module.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Module.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Module.Infrastructure.Repositories
{
    public class SessionStoreRepository : ISessionStoreRepository
    {
        private readonly WriteDbContext _context;
        private readonly ILogger<SessionStoreRepository> _logger;

        public SessionStoreRepository(WriteDbContext context, ILogger<SessionStoreRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket, string userId, string ipAddress, string userAgent)
        {
            try
            {
                _logger.LogInformation("Almacenando nueva sesión para el usuario {UserId}", userId);

                var ticketBytes = TicketSerializer.Default.Serialize(ticket);
                var ticketBase64 = Convert.ToBase64String(ticketBytes);

                var session = new SessionTicket
                {
                    UserId = userId,
                    SessionKey = Guid.NewGuid().ToString(),
                    TicketData = ticketBase64,
                    CreatedAt = DateTime.UtcNow,
                    LastActivity = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(3),
                    IpAddress = ipAddress,
                    UserAgent = userAgent
                };

                await _context.SessionTickets.AddAsync(session);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Sesión almacenada con éxito. SessionKey: {SessionKey} para el usuario {UserId}", session.SessionKey, userId);
                return session.SessionKey;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al almacenar la sesión para el usuario {UserId}", userId);
                return string.Empty;
            }
        }

        public async Task<bool> RenewAsync(string key, AuthenticationTicket ticket)
        {
            try
            {
                var session = await _context.SessionTickets.FirstOrDefaultAsync(s => s.SessionKey == key);
                if (session == null || session.IsSessionExpired)
                {
                    _logger.LogWarning("Renovación fallida: la sesión {SessionKey} no existe o ha expirado.", key);
                    return false;
                }

                _logger.LogInformation("Renovando sesión {SessionKey} para el usuario {UserId}", key, session.UserId);
                session.LastActivity = DateTime.UtcNow;
                session.ExpiresAt = DateTime.UtcNow.AddMinutes(10);

                var ticketBytes = TicketSerializer.Default.Serialize(ticket);
                session.TicketData = Convert.ToBase64String(ticketBytes);

                await _context.SaveChangesAsync();
                _logger.LogInformation("Sesión {SessionKey} renovada con éxito.", key);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al renovar la sesión {SessionKey}", key);
                return false;
            }
        }

        public async Task<AuthenticationTicket?> RetrieveAsync(string key)
        {
            try
            {
                var session = await _context.SessionTickets.FirstOrDefaultAsync(s => s.SessionKey == key);
                if (session == null || session.IsSessionExpired)
                {
                    _logger.LogWarning("Intento de recuperar sesión fallido. La sesión {SessionKey} no existe o ha expirado.", key);
                    return null;
                }

                var ticketBytes = Convert.FromBase64String(session.TicketData);
                return TicketSerializer.Default.Deserialize(ticketBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar la sesión {SessionKey}", key);
                return null;
            }
        }

        public async Task<bool> RemoveAsync(string key)
        {
            try
            {
                var session = await _context.SessionTickets.FirstOrDefaultAsync(s => s.SessionKey == key);
                if (session == null)
                {
                    _logger.LogWarning("Eliminación fallida: la sesión {SessionKey} no existe.", key);
                    return false;
                }

                _logger.LogInformation("Eliminando sesión {SessionKey} del usuario {UserId}", key, session.UserId);
                _context.SessionTickets.Remove(session);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Sesión {SessionKey} eliminada con éxito.", key);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la sesión {SessionKey}", key);
                return false;
            }
        }
   
        public async Task<bool> ExpireSessionAsync(string key)
        {
            try
            {
                var session = await _context.SessionTickets.FirstOrDefaultAsync(s => s.SessionKey == key);
                if (session == null)
                {
                    _logger.LogWarning("Intento de expirar sesión fallido. La sesión {SessionKey} no existe.", key);
                    return false;
                }

                _logger.LogInformation("Expirando sesión {SessionKey} del usuario {UserId}", key, session.UserId);
                session.IsSessionExpired = true;
                session.LogoutAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al expirar la sesión {SessionKey}", key);
                return false;
            }
        }

        public async Task<bool> ExpireInactiveSessionsAsync()
        {
            try
            {
                var now = DateTime.UtcNow;
                var expiredSessions = await _context.SessionTickets
                    .Where(st => st.ExpiresAt <= now && !st.IsSessionExpired)
                    .ToListAsync();

                if (!expiredSessions.Any())
                {
                    _logger.LogInformation("No hay sesiones inactivas para expirar.");
                    return false;
                }

                _logger.LogInformation("Expirando {SessionCount} sesiones inactivas.", expiredSessions.Count);
                foreach (var session in expiredSessions)
                {
                    session.IsSessionExpired = true;
                    session.LogoutAt = now;
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Todas las sesiones inactivas han sido expiradas con éxito.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al expirar sesiones inactivas.");
                return false;
            }
        }

        public async Task<bool> RevokeSessionByAdminAsync(string key)
        {
            try
            {
                var session = await _context.SessionTickets.FirstOrDefaultAsync(s => s.SessionKey == key);
                if (session == null)
                {
                    _logger.LogWarning("Intento de revocar sesión fallido. La sesión {SessionKey} no existe.", key);
                    return false;
                }

                _logger.LogInformation("Revocando sesión {SessionKey} por un administrador", key);
                session.IsSessionExpired = true;
                session.LogoutAt = DateTime.UtcNow;
                session.RevokedByAdmin = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al revocar la sesión {SessionKey} por un administrador", key);
                return false;
            }
        }

        public async Task<bool> UpdateSessionAsync(SessionTicket session)
        {
            try
            {
                var existingSession = await _context.SessionTickets.FirstOrDefaultAsync(s => s.SessionKey == session.SessionKey);
                if (existingSession == null)
                {
                    _logger.LogWarning("Actualización fallida: la sesión {SessionKey} no existe.", session.SessionKey);
                    return false;
                }

                _logger.LogInformation("Actualizando sesión {SessionKey} del usuario {UserId}", session.SessionKey, session.UserId);
                _context.SessionTickets.Update(session);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Sesión {SessionKey} actualizada con éxito.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la sesión {SessionKey}", session.SessionKey);
                return false;
            }
        }

        public async Task<SessionTicket?> RetrieveSessionAsync(string key)
        {
            try
            {
                var session = await _context.SessionTickets.FirstOrDefaultAsync(s => s.SessionKey == key);
                if (session == null)
                    _logger.LogWarning("Intento de recuperar sesión fallido. La sesión {SessionKey} no existe.", key);

                return session;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar la sesión {SessionKey}", key);
                return null;
            }
        }
    }
}
