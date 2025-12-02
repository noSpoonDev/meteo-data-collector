using MeteoDataCollector.Core.Contracts.Repositories;
using MeteoDataCollector.Infrastructure.Contexts;
using MeteoDataCollector.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MeteoDataCollector.App;

class Program
{
    static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .UseSerilog((_, config) =>
            {
                config.MinimumLevel.Information();
                config.WriteTo.Console(outputTemplate:
                    "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u4}] {Message:lj}{NewLine}{Exception}");
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
                services.AddDbContext<MeteoDataCollectorDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DBConnection")));

                services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            })
            .Build();

        using var migrationScope = host.Services.CreateScope();
        var dbContext = migrationScope.ServiceProvider.GetRequiredService<MeteoDataCollectorDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}