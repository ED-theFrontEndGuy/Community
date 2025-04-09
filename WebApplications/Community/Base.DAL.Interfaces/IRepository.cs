using Base.Domain;

namespace Base.DAL.Interfaces;

public interface IRepository<TEntity> : IRepository<TEntity, Guid>
    where TEntity : BaseEntity
{
}

public interface IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    IEnumerable<TEntity> All(TKey? userId);
    Task<IEnumerable<TEntity>> AllAsync(TKey? userId);

    TEntity Find(TKey id, TKey? userId);
    Task<TEntity> FindAsync(TKey id, TKey? userId);

    void Add(TEntity entity);

    TEntity Update(TEntity entity);

    void Remove(TEntity entity, TKey? userId);

    void Remove(TKey id, TKey? userId);
}