namespace MeteoDataCollector.Core.Services;

public interface IMeteoDataFetcher
{
    Task<string?> FetchLatestData(CancellationToken cancellationToken = default);
}