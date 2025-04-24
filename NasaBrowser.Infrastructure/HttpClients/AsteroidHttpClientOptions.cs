namespace NasaBrowser.Infrastructure.HttpClients;

public class AsteroidHttpClientOptions
{
    public readonly string BaseAddress;
    public readonly string Resource;

    public AsteroidHttpClientOptions(string baseAddress, string resource)
    {
        BaseAddress = baseAddress;
        Resource = resource;
    }
}