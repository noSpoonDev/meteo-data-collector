using MeteoDataCollector.Core.Contracts.Repositories;
using MeteoDataCollector.Core.Models;

namespace MeteoDataCollector.Core.Services;

public class MeteoDataProcessingService : IMeteoDataProcessingService
{
    private readonly IMeteoDataFetcher _fetcher;
    private readonly ITransformToJsonStrategy _transformer;
    private readonly IRepositoryWrapper _repository;

    public MeteoDataProcessingService(
        IMeteoDataFetcher fetcher,
        ITransformToJsonStrategy transformer,
        IRepositoryWrapper repository)
    {
        _fetcher = fetcher;
        _transformer = transformer;
        _repository = repository;
    }

    public async Task Process(CancellationToken cancellationToken = default)
    {
        var data = await _fetcher.FetchLatestData(cancellationToken);
        if (data != null)
        {
            data = _transformer.Transform(data);
        }

        var record = new MeteoDataRecord
        {
            IsStationOnline = data != null,
            JsonData = data
        };
        await _repository.MeteoDataRecord.Create(record);
        await _repository.SaveAsync();
    }
}