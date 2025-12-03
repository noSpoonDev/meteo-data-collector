namespace MeteoDataCollector.Core.Services;

public interface IMeteoDataProcessingService
{
    Task Process(CancellationToken cancellationToken = default);
}