using Microsoft.Extensions.Logging;

namespace MeteoDataCollector.Core.Services;

public class MeteoDataFetcher : IMeteoDataFetcher
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MeteoDataFetcher> _logger;

    public MeteoDataFetcher(HttpClient httpClient, ILogger<MeteoDataFetcher> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string?> FetchLatestData(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(cancellationToken);
            }

            _logger.LogError(
                "Failed to fetch meteo data. HTTP status code: {StatusCode} ({ReasonPhrase})",
                (int)response.StatusCode, response.ReasonPhrase);
            return null;
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(
                "Request to meteo station timed out after {Timeout} seconds",
                _httpClient.Timeout.TotalSeconds
            );
            return null;
        }
    }
}