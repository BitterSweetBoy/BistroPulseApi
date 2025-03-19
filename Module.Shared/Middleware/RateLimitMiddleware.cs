using Microsoft.AspNetCore.Http;
using Module.Shared.Response;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Module.Shared.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, (DateTime Timestamp, int Count)> _requestCounts = new();
        private const int Limit = 20; // Máximo de solicitudes permitidas
        private static readonly TimeSpan TimeWindow = TimeSpan.FromSeconds(10); // Tiempo de ventana
        public RateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress))
            {
                await _next(context);
                return;
            }

            var now = DateTime.UtcNow;
            _requestCounts.AddOrUpdate(ipAddress, (now, 1), (_, entry) =>
            {
                if (now - entry.Timestamp > TimeWindow)
                    return (now, 1);
                return (entry.Timestamp, entry.Count + 1);
            });

            if(_requestCounts[ipAddress].Count > Limit)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                var response = new ApiResponse<object>(
                    Success: false,
                    Message: "Demasiadas solicitudes",
                    Data: null,
                    Errors: new Dictionary<string, string[]>
                    {
                        { "Limite de peticiones", new[] { "Ha excedido el límite de peticiones" } }
                    }
                );
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }
            await _next(context);
        }
    }
}
