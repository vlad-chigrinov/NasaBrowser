using System.Net.Http.Json;
using NasaBrowser.Application.Exceptions;
using NasaBrowser.Application.Models.AsteroidJsonModel;
using NasaBrowser.Domain.Common;
using NasaBrowser.Infrastructure.Exceptions;

namespace NasaBrowser.Infrastructure.HttpClients;

public class AsteroidHttpClient : IDataSource<Task<IEnumerable<AsteroidJsonDTO>>>
{
    private readonly HttpClient _httpClient;
    private readonly AsteroidHttpClientOptions _options;

    public AsteroidHttpClient(HttpClient httpClient, AsteroidHttpClientOptions options)
    {
        _httpClient = httpClient;
        _options = options;
        _httpClient.BaseAddress = new Uri(options.BaseAddress);
    }

    public virtual async Task<IEnumerable<AsteroidJsonDTO>> Get()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_options.Resource);

        if (response.IsSuccessStatusCode == false)
            throw new DataSourceResponseException("External resource is not responding");

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<AsteroidJsonDTO>>();

        if (result == null)
            throw new DataSourceConvertException("Failed to process a response from an external resource");

        return result;
    }
}