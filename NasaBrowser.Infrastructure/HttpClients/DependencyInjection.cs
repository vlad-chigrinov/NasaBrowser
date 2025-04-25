using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Application.Models.AsteroidJsonModel;
using NasaBrowser.Domain.Common;
using Polly;
using Polly.Extensions.Http;

namespace NasaBrowser.Infrastructure.HttpClients;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AsteroidHttpClientOptions>(configuration.GetSection(nameof(AsteroidHttpClientOptions)));
        
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError() 
            .WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        services.AddHttpClient<IDataSource<IEnumerable<AsteroidJsonDTO>>, AsteroidHttpClient>()
            .AddPolicyHandler(retryPolicy);

        return services;
    }
}