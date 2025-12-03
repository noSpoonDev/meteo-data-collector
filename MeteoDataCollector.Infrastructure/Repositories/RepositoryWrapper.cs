using MeteoDataCollector.Core.Contracts.Repositories;
using MeteoDataCollector.Infrastructure.Contexts;

namespace MeteoDataCollector.Infrastructure.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private IMeteoDataRecordRepository? _meteoDataRecordRepository;
    private MeteoDataCollectorDbContext _dbContext;

    public RepositoryWrapper(MeteoDataCollectorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IMeteoDataRecordRepository MeteoDataRecord =>
        _meteoDataRecordRepository ??= new MeteoDataRecordRepository(_dbContext);

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}