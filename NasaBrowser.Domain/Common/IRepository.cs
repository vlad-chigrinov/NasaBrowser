namespace NasaBrowser.Domain.Common;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
    
    public void Add(T entity);
    
    public void Remove(T entity);

    public Task SaveChangesAsync(CancellationToken ct = default);
}