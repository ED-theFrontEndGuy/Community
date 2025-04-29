using Base.Interfaces;

namespace Base.DAL.Interfaces;

public interface IRepository<TEntity> : IRepository<TEntity, Guid>
    where TEntity : IDomainId
{
}

public interface IRepository<TEntity, TKey>
    where TEntity : IDomainId<TKey>
    where TKey : IEquatable<TKey>
{
    // TODO: Implement UOW, remove SaveChangesAsync from repository
    Task<int> SaveChangesAsync();
    
    IEnumerable<TEntity> All(TKey? userId = default!);
    Task<IEnumerable<TEntity>> AllAsync(TKey? userId = default!);

    TEntity? Find(TKey id, TKey? userId = default!);
    Task<TEntity?> FindAsync(TKey id, TKey? userId = default!);

    void Add(TEntity entity);

    TEntity Update(TEntity entity);

    void Remove(TEntity entity, TKey? userId = default!);
    
    void Remove(TKey id, TKey? userId);
    Task RemoveAsync(TKey id, TKey? userId = default!);

    bool Exists(TKey id, TKey? userId = default!);
    Task<bool> ExistsAsync(TKey id, TKey? userId = default!);
}