using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NasaBrowser.Api.DataAccess;
using NasaBrowser.Api.DBOs;

namespace NasaBrowser.Api.BackgroundServices;

public sealed class NasaDataSyncService : BackgroundService
{
    private readonly HttpClient _httpClient;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NasaDataSyncService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(10);
    private readonly PeriodicTimer _timer;

    public NasaDataSyncService(
        HttpClient httpClient,
        IServiceProvider serviceProvider,
        ILogger<NasaDataSyncService> logger)
    {
        _httpClient = httpClient;
        _serviceProvider = serviceProvider;
        _logger = logger;
        _timer = new PeriodicTimer(_interval);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            // Первый запуск сразу
            await DoWorkAsync(stoppingToken);

            using var timer = new PeriodicTimer(_interval);
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await DoWorkAsync(stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Игнорируем, если задача отменена
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в фоновой задаче");
        }
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            var jsonData = await FetchDataAsync(stoppingToken);
            await SyncDataAsync(dbContext, jsonData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка синхронизации данных");
        }
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
}