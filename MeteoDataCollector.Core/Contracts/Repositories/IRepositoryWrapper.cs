namespace MeteoDataCollector.Core.Contracts.Repositories;

public interface IRepositoryWrapper
{
    IMeteoDataRecordRepository MeteoDataRecord { get; }
    Task SaveAsync();
}