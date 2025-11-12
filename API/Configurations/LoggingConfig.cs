using Serilog;
using Serilog.Formatting.Compact;

namespace API.Configurations;

public static class LoggingConfig
{
    public static IHostBuilder UseAppLogging(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.File(
                new CompactJsonFormatter(),
                Path.Combine("..", "Logs", "log-.json"),
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 10 * 1024 * 1024,
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: 30,
                shared: true
            )
            .CreateLogger();

        hostBuilder.UseSerilog();
        return hostBuilder;
    }
}