using Module.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Module.Core.Entities;
using Module.Application.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace Module.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ISessionStore _sessionStore;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ISessionStore sessionStore, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sessionStore = sessionStore;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(RegisterDto model)
        {
            _logger.LogInformation("Inicio del registro para el usuario: {Email}", model.Email);
            try
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Registro exitoso para el usuario: {Email}", model.Email);
                    return true;
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("Fallo el registro para el usuario: {Email}. Errores: {Errors}", model.Email, errors);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar el usuario {Email}", model.Email);
                return false;
            }
        }

        public async Task<bool> LoginAsync(LoginDto model)
        {
            _logger.LogInformation("Intentando iniciar sesión con Email: {Email}", model.Email);
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    _logger.LogWarning("Inicio de sesión fallido: usuario no encontrado para Email: {Email}", model.Email);
                    return false;
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Inicio de sesión fallido: contraseña incorrecta para Email: {Email}", model.Email);
                    return false;
                }

                _logger.LogInformation("Contraseña verificada para Email: {Email}. Generando sesión...", model.Email);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var httpContext = _signInManager.Context;
                var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "SessionId");
                var principal = new ClaimsPrincipal(claimsIdentity);
                var ticket = new AuthenticationTicket(principal, authProperties, "SessionId");

                var sessionKey = await _sessionStore.StoreAsync(ticket, user.Id.ToString(), ipAddress, userAgent);
                if (string.IsNullOrEmpty(sessionKey))
                {
                    _logger.LogWarning("No se pudo generar la sesión para el usuario: {Email}", model.Email);
                    return false;
                }

                claims.Add(new Claim("SessionKey", sessionKey));
                claimsIdentity = new ClaimsIdentity(claims, "SessionId");
                principal = new ClaimsPrincipal(claimsIdentity);

                await _signInManager.Context.SignInAsync("SessionId", principal, authProperties);
                _logger.LogInformation("Inicio de sesión exitoso para Email: {Email}", model.Email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el inicio de sesión para el usuario {Email}", model.Email);
                return false;
            }
        }

        public async Task<bool> LogoutAsync()
        {
            var user = _signInManager.Context.User;
            if (user?.Identity?.IsAuthenticated != true)
            {
                _logger.LogWarning("Intento de cierre de sesión fallido: no hay usuario autenticado.");
                return false;
            }

            var userEmail = user.FindFirst(ClaimTypes.Email)?.Value ?? "Desconocido";
            _logger.LogInformation("Cierre de sesión iniciado para el usuario con Email: {Email}", userEmail);

            try
            {
                var sessionKeyClaim = user.FindFirst("SessionKey");
                if (sessionKeyClaim != null)
                {
                    var sessionKey = sessionKeyClaim.Value;
                    var sessionExpired = await _sessionStore.ExpireSessionAsync(sessionKey);

                    if (sessionExpired)
                    {
                        _logger.LogInformation("Sesión expirada para el usuario {Email}, SessionKey: {SessionKey}", userEmail, sessionKey);
                    }
                    else
                    {
                        _logger.LogWarning("No se pudo expirar la sesión para el usuario {Email}, SessionKey: {SessionKey}", userEmail, sessionKey);
                    }
                }
                else
                {
                    _logger.LogWarning("No se encontró SessionKey para el usuario {Email}", userEmail);
                }

                await _signInManager.SignOutAsync();
                await _signInManager.Context.SignOutAsync("SessionId");

                _logger.LogInformation("Cierre de sesión exitoso para el usuario con Email: {Email}", userEmail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cerrar sesión para el usuario {Email}", userEmail);
                return false;
            }
        }
    }
}
