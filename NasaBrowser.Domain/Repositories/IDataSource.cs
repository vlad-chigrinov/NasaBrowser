namespace NasaBrowser.Domain.Repositories;

public interface IDataSource<T>
{
    public Task<T> GetAsync(CancellationToken cancellationToken = default);
}