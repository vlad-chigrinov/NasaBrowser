namespace NasaBrowser.Domain.Common;

public interface IQueryExecutor<T>
{
    public Task<IEnumerable<T>> ExecuteAsync(IQueryable<T> queryable, CancellationToken ct = default);
}