namespace NasaBrowser.Domain.Repositories;

public interface IRepository<TEntity, TKey>
{
    public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default);
    public Task<HashSet<TKey>> GetIdentifiersAsync(CancellationToken ct = default);

    public void AddRange(IEnumerable<TEntity> entities);
    public void RemoveRange(IEnumerable<TKey> ids);
    
    public Task<int> SaveChangesAsync();
    public Task BeginTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}