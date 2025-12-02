namespace MeteoDataCollector.Core.Services;

public interface ITransformToJsonStrategy
{
    string Transform(string input);
}