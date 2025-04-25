using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using NasaBrowser.Application.Exceptions;
using NasaBrowser.Application.Models.AsteroidJsonModel;
using NasaBrowser.Domain.Common;

namespace NasaBrowser.Infrastructure.HttpClients;

public class AsteroidHttpClient : IDataSource<IEnumerable<AsteroidJsonDTO>>
{
    private readonly HttpClient _httpClient;
    private readonly AsteroidHttpClientOptions _options;

    public AsteroidHttpClient(HttpClient httpClient, IOptions<AsteroidHttpClientOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _httpClient.BaseAddress = new Uri(_options.BaseAddress);
    }

    public async Task<IEnumerable<AsteroidJsonDTO>> GetAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_options.Resource);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<AsteroidJsonDTO>>();

        if (result == null)
            throw new DataSourceConvertException("Failed to process a response from an external resource");

        return result;
    }
}