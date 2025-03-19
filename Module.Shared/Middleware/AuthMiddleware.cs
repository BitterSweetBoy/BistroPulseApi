using Microsoft.AspNetCore.Http;
using Module.Shared.Response;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Module.Shared.Interfaces;

namespace Module.Shared.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly HashSet<string> _excludedRoutes = new()
        {
            "/auth/register",
            "/auth/login"
        };
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Excluir ciertas rutas de la validación de sesión
            if (_excludedRoutes.Contains(context.Request.Path.Value))
            {
                await _next(context);
                return;
            }
            // Obtener el servicio de sesión directamente del contexto
            var sessionStore = context.RequestServices.GetRequiredService<ISessionValidator>();

            if (!await sessionStore.ValidateSessionAsync())
            {
                await HandleUnauthorizedResponse(context);
                return;
            }
            await _next(context);
        }


        private static async Task HandleUnauthorizedResponse(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object>(
                Success: false,
                Message: "Sesión no válida o expirada",
                Data: null,
                Errors: new Dictionary<string, string[]>
                {
                    { "Session", new[] { "Debe iniciar sesión nuevamente" } }
                }
            );
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}