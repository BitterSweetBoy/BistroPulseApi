
using Microsoft.AspNetCore.Http;
using Module.Shared.Response;
using System.Text.Json;

namespace Module.Shared.Middleware
{
    public class RequestTimeoutMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);

        public RequestTimeoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using var cts = new CancellationTokenSource(Timeout);
            var task = _next(context);
            var completedTask = await Task.WhenAny(task, Task.Delay(Timeout, cts.Token));

            if (completedTask != task)
            {
                context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
                var response = new ApiResponse<object>(
                    Success: false,
                    Message: "Tiempo de espera agotado",
                    Data: null,
                    Errors: new Dictionary<string, string[]>
                    {
                        { "Timeout", new[] { "El servidor tardó demasiado en responder" } }
                    }
                );
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }

            await task;
        }

    }
}
