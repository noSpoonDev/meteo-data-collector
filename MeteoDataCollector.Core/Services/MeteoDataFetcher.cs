using MeteoDataCollector.Core.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MeteoDataCollector.Core.Services;

public class MeteoDataFetcher : IMeteoDataFetcher
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<MeteoDataFetcher> _logger;
    private readonly IOptions<MeteoDataSourceSettings> _settings;

    public MeteoDataFetcher(IHttpClientFactory httpClientFactory, ILogger<MeteoDataFetcher> logger,
        IOptions<MeteoDataSourceSettings> settings)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _settings = settings;
    }

    public async Task<string?> FetchLatestData(CancellationToken cancellationToken = default)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(_settings.Value.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("Accept", _settings.Value.AcceptHeader);

            var response = await client.GetAsync(_settings.Value.Url, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(cancellationToken);
            }

            _logger.LogError(
                "Failed to fetch meteo data. HTTP status code: {StatusCode} ({ReasonPhrase})",
                (int)response.StatusCode, response.ReasonPhrase);
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(ex,
                "Request to meteo station timed out after {Timeout} seconds",
                _settings.Value.TimeoutSeconds
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Request to meteo station failed: {Message}", ex.Message
            );
        }

        return null;
    }
}