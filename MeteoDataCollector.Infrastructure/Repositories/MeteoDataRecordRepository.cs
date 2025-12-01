using MeteoDataCollector.Core.Contracts.Repositories;
using MeteoDataCollector.Core.Models;
using MeteoDataCollector.Infrastructure.Contexts;

namespace MeteoDataCollector.Infrastructure.Repositories;

public class MeteoDataRecordRepository : IMeteoDataRecordRepository
{
    private MeteoDataCollectorDbContext _dbContext;
    
    public MeteoDataRecordRepository(MeteoDataCollectorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(MeteoDataRecord record)
    {
        await _dbContext.Set<MeteoDataRecord>().AddAsync(record);
    }
}