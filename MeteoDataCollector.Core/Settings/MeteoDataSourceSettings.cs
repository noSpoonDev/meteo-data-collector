namespace MeteoDataCollector.Core.Settings;

public class MeteoDataSourceSettings
{
    public string Url { get; set; } = null!;
    public int TimeoutSeconds { get; set; } = 15;
    public string AcceptHeader { get; set; } = "application/xml";
}