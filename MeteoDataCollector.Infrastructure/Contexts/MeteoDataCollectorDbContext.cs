using MeteoDataCollector.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MeteoDataCollector.Infrastructure.Contexts;

public class MeteoDataCollectorDbContext : DbContext
{
    public DbSet<MeteoDataRecord> MeteoDataRecords { get; set; } = null!;

    public MeteoDataCollectorDbContext(DbContextOptions opt) : base(opt)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeteoDataRecord>()
            .ToTable("meteo_data_record");
    }
}