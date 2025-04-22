using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NasaBrowser.Api.DataAccess;
using NasaBrowser.Api.DBOs;
using Quartz;

namespace NasaBrowser.Api.BackgroundServices;

[DisallowConcurrentExecution]
public sealed class NasaDataSyncJob : IJob
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<NasaDataSyncJob> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(10);
    private readonly ApplicationDbContext _dbContext;

    public NasaDataSyncJob(
        HttpClient httpClient,
        IServiceProvider serviceProvider,
        ApplicationDbContext dbContext,
        ILogger<NasaDataSyncJob> logger)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
        _logger = logger;
    }

    private async Task<IEnumerable<Meteorite>> FetchDataAsync(CancellationToken ct)
    {
        var response = await _httpClient.GetAsync("https://raw.githubusercontent.com/biggiko/nasa-dataset/refs/heads/main/y77d-th95.json", ct);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync(ct);
        var meteorites = JsonSerializer.Deserialize<List<MeteoriteDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return meteorites.Select(meteorite => meteorite.ToEntity());
    }

    private async Task SyncDataAsync(ApplicationDbContext dbDbContext, IEnumerable<Meteorite> jsonItems)
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
        try
        {
            var jsonData = await FetchDataAsync(context.CancellationToken);
            await SyncDataAsync(_dbContext, jsonData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка синхронизации данных");
        }
    }
}