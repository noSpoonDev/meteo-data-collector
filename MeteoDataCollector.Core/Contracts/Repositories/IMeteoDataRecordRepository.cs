using MeteoDataCollector.Core.Models;

namespace MeteoDataCollector.Core.Contracts.Repositories;

public interface IMeteoDataRecordRepository
{
    Task Create(MeteoDataRecord record);
}