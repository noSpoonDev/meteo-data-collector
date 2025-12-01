using MeteoDataCollector.Core.Contracts.Repositories;
using MeteoDataCollector.Infrastructure.Contexts;
using MeteoDataCollector.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeteoDataCollector.App;

class Program
{
    static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
                services.AddDbContext<MeteoDataCollectorDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DBConnection")));

                services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            })
            .Build();
    }
}