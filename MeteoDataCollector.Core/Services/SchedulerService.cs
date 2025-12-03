using MeteoDataCollector.Core.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MeteoDataCollector.Core.Services;

public class SchedulerService : BackgroundService
{
    private readonly IMeteoDataProcessingService _processingService;
    private readonly ILogger<SchedulerService> _logger;
    private readonly IOptions<MeteoDataSourceSettings> _settings;

    public SchedulerService(IMeteoDataProcessingService processingService, ILogger<SchedulerService> logger,
        IOptions<MeteoDataSourceSettings> settings)
    {
        _processingService = processingService;
        _logger = logger;
        _settings = settings;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Scheduler service started.");

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await _processingService.Process(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in meteo data processing: {Message}", ex.Message);
            }

            try
            {
                await Task.Delay(TimeSpan.FromMinutes(_settings.Value.FetchFrequencyMinutes), cancellationToken);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Shutdown requested");
            }
        }

        _logger.LogInformation("Scheduler service stopped.");
    }
}