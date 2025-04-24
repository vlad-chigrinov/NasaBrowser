using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NasaBrowser.Api.DBOs;
using NasaBrowser.Infrastructure.Database;
using Quartz;

namespace NasaBrowser.Api.BackgroundServices;

[DisallowConcurrentExecution]
public sealed class NasaDataSyncJob : IJob
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<NasaDataSyncJob> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(10);
    private readonly AsteroidsDbContext _dbContext;

    public NasaDataSyncJob(
        HttpClient httpClient,
        IServiceProvider serviceProvider,
        AsteroidsDbContext dbContext,
        ILogger<NasaDataSyncJob> logger)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
        _logger = logger;
    }

    private async Task SyncDataAsync(AsteroidsDbContext dbDbContext, IEnumerable<Meteorite> jsonItems)
    {
        var dbItems = dbDbContext.Meteorites.ToDictionary(x => x.Id);
        var jsonIds = jsonItems.Select(x => x.Id).ToHashSet();

        foreach (var jsonItem in jsonItems)
        {
            if (dbItems.TryGetValue(jsonItem.Id, out var existing) == false)
            {
                dbDbContext.Meteorites.Add(jsonItem);
            }
        }

        var toDelete = dbItems.Values.Where(x => !jsonIds.Contains(x.Id));
        dbDbContext.Meteorites.RemoveRange(toDelete);
        await dbDbContext.SaveChangesAsync();
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var jsonData = await FetchDataAsync(context.CancellationToken);
        await SyncDataAsync(_dbContext, jsonData);
    }
}