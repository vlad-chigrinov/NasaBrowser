using Microsoft.Extensions.Caching.Hybrid;
using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Infrastructure.Database.Repositories;

public class AsteroidsGroupCacheRepository : IAsteroidsGroupCacheRepository
{
    private readonly HybridCache _hybridCache;

    private const string DefaultTag = nameof(AsteroidsGroupCacheRepository);

    public AsteroidsGroupCacheRepository(HybridCache hybridCache)
    {
        _hybridCache = hybridCache;
    }

    public async Task<IEnumerable<AsteroidsGroupResponse>> GetOrCreateAsync(AsteroidsGroupRequest request,
        Func<CancellationToken, ValueTask<IEnumerable<AsteroidsGroupResponse>>> func,
        CancellationToken cancellationToken)
    {
        return await _hybridCache.GetOrCreateAsync(request.GetUniqueKey(), func,
            options: new HybridCacheEntryOptions { Expiration = TimeSpan.FromMinutes(5) },
            tags: [DefaultTag],
            cancellationToken: CancellationToken.None);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _hybridCache.RemoveByTagAsync(DefaultTag, cancellationToken);
    }
}