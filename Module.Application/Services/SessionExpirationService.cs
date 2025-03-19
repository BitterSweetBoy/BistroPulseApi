using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Module.Application.Interfaces;

namespace Module.Application.Services
{
    public class SessionExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(15);
        private readonly ILogger<SessionExpirationService> _logger;
        public SessionExpirationService(IServiceScopeFactory serviceScopeFactory, ILogger<SessionExpirationService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        _logger.LogInformation("Inciando proceso automatico para limpiar lase sesiones");
                        var sessionRepository = scope.ServiceProvider.GetRequiredService<ISessionStoreRepository>();
                        await sessionRepository.ExpireInactiveSessionsAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al ejecutar el proceso automatico para expirar sesiones inactivas.");
                }
                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
