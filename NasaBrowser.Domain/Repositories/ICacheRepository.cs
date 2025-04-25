using NasaBrowser.Domain.Contracts.Requests;

namespace NasaBrowser.Domain.Repositories;

public interface ICacheRepository<TRequest, IResponse> where TRequest : IUserQuery
{
    public Task<IResponse> GetOrCreateAsync(TRequest request,
        Func<CancellationToken, ValueTask<IResponse>> func,
        CancellationToken cancellationToken = default);

    public Task ClearAsync(CancellationToken cancellationToken = default);
}