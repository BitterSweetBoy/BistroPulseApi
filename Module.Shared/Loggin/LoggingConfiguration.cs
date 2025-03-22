using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Module.Shared.Logs
{
    public static class LoggingConfiguration
    {
        public static void AddCustomLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(configuration["Serilog:SeqServerUrl"])
                .CreateLogger();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
                Console.WriteLine("Logging configured");
            });
        }
    }
}