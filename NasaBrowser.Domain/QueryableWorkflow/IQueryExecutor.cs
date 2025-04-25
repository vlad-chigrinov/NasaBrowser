namespace NasaBrowser.Domain.QueryableWorkflow;

public interface IQueryExecutor<T>
{
    public Task<IEnumerable<T>> ExecuteAsync(IQueryable<T> queryable, CancellationToken ct = default);
}