namespace NasaBrowser.Domain.Common;

public interface IDataSource<T>
{
    public Task<T> GetAsync();
}