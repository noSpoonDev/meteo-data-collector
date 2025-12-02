using MeteoDataCollector.App.Settings;
using MeteoDataCollector.Core.Contracts.Repositories;
using MeteoDataCollector.Core.Services;
using MeteoDataCollector.Infrastructure.Contexts;
using MeteoDataCollector.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
                
                services.Configure<MeteoDataSourceSettings>(configuration.GetSection("MeteoDataSourceSettings"));
                services.AddHttpClient<IMeteoDataFetcher, MeteoDataFetcher>((sp, client) =>
                {
                    var settings = sp.GetRequiredService<IOptions<MeteoDataSourceSettings>>().Value;
                    client.BaseAddress = new Uri(settings.Url);
                    client.Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds);
                    client.DefaultRequestHeaders.Add("Accept", settings.AcceptHeader);
                });

                services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            })
            .Build();

        #region Apply migrations
        
        using var migrationScope = host.Services.CreateScope();
        var dbContext = migrationScope.ServiceProvider.GetRequiredService<MeteoDataCollectorDbContext>();
        await dbContext.Database.MigrateAsync();

        #endregion
    }
}