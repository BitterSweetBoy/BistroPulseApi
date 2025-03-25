using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Module.Application.Interfaces;
using Module.Core.Entities;
using Module.Infrastructure.Data;

namespace Module.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, string connectionString)
        {
            // Configurar el DbContext
            services.AddDbContext<WriteDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WriteDbContext>()
                .AddDefaultTokenProviders();

            // Configurar la autenticación con un solo esquema "SessionId"
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "SessionId";
                options.DefaultChallengeScheme = "SessionId";
                options.DefaultSignInScheme = "SessionId";
            })
            .AddCookie("SessionId", options =>
            {
                ConfigureCookieOptions(options);
            });
        }

        private static void ConfigureCookieOptions(CookieAuthenticationOptions options)
        {
            // Configuración de la cookie de sesión
            options.Cookie.Name = "SessionId";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Expira en 30 minutos
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Requiere HTTPS (Production)
            options.Cookie.SameSite = SameSiteMode.None; // Aqui se configura el uso en contextos de terceros
            options.LoginPath = "/auth/login";
            options.LogoutPath = "/auth/logout";
            options.SlidingExpiration = true; // Renueva la expiración si hay actividad

            // Evento para validar y renovar la sesión si es necesario
            options.Events.OnValidatePrincipal = async context =>
            {
                var now = DateTimeOffset.UtcNow;
                var expiresUtc = context.Properties.ExpiresUtc;

                // Si la sesión está por expirar en menos de 5 minutos, renovar
                if (expiresUtc.HasValue && now > expiresUtc.Value.AddMinutes(-5))
                {
                    await RenewSessionAsync(context);
                }
            };
        }
        private static async Task RenewSessionAsync(CookieValidatePrincipalContext context)
        {
            var httpContext = context.HttpContext;
            Console.WriteLine("Renovando sesión para el usuario:");

            // Extender la expiración de la cookie en 45 minutos
            context.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(45);
            await httpContext.SignInAsync(context.Principal, context.Properties);

            // Obtener el SessionKey del claim del usuario autenticado
            var sessionKey = context.Principal.FindFirst("SessionKey")?.Value;
            if (string.IsNullOrEmpty(sessionKey)) return;

            // Obtener el servicio de sesiones desde el contenedor de dependencias
            var sessionStore = httpContext.RequestServices.GetService<ISessionStore>();
            if (sessionStore == null) return;

            // Verificar si la sesión sigue activa en la base de datos
            var storedTicket = await sessionStore.RetrieveAsync(sessionKey);
            if (storedTicket == null)
            {
                // Si la sesión no es válida, cerrar sesión
                context.RejectPrincipal();
                await httpContext.SignOutAsync(context.Scheme.Name);
            }
            else
            {
                // Si la sesión sigue activa, renovar en la base de datos
                var ticket = new AuthenticationTicket(context.Principal, context.Properties, context.Scheme.Name);
                await sessionStore.RenewAsync(sessionKey, ticket);
            }
        }

    }
}
