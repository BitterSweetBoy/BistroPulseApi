// Module.Application/Services/SessionStoreService.cs
using Module.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Module.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using Module.Core.Entities;
using Microsoft.Extensions.Logging;

namespace Module.Application.Services
{
    public class SessionStoreService : ISessionStore, ISessionValidator
    {
        private readonly ISessionStoreRepository _repository;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<SessionStoreService> _logger;

        public SessionStoreService(ISessionStoreRepository repository, SignInManager<User> signInManager, ILogger<SessionStoreService> logger)
        {
            _repository = repository;
            _signInManager = signInManager;
            _logger = logger;
        }

        public Task<string> StoreAsync(AuthenticationTicket ticket, string userId, string ipAddress, string userAgent)
        {
            return _repository.StoreAsync(ticket, userId, ipAddress, userAgent);
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            return _repository.RenewAsync(key, ticket);
        }

        public Task<AuthenticationTicket?> RetrieveAsync(string key)
        {
            return _repository.RetrieveAsync(key);
        }

        public Task<bool> ExpireSessionAsync(string key)
        {
            return _repository.ExpireSessionAsync(key);
        }

        public Task<bool> RevokeSessionByAdminAsync(string key)
        {
            return _repository.RevokeSessionByAdminAsync(key);
        }

        public async Task<bool> ValidateSessionAsync()
        {
            try
            {
                _logger.LogInformation("Validando la sesión del usuario");
                var user = _signInManager.Context.User;
                var sessionKeyClaim = user.FindFirst("SessionKey");
                if (sessionKeyClaim == null)
                    return false;
                var sessionKey = sessionKeyClaim.Value;
                var session = await _repository.RetrieveSessionAsync(sessionKey);
                if (session == null || session.IsSessionExpired || session.RevokedByAdmin || session.ExpiresAt <= DateTime.UtcNow)
                {
                    return false;
                }

                // Solo actualizar la última actividad si es necesario (por ejemplo, si ha pasado mucho tiempo desde la última actualización)
                if (session.LastActivity.AddMinutes(5) <= DateTime.UtcNow)
                {
                    session.LastActivity = DateTime.UtcNow;
                    await _repository.UpdateSessionAsync(session);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar la sesión del usuario");
                return false;
            }
        }

    }
}
