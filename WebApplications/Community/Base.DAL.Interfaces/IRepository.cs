using Base.Domain;

namespace Base.DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IEnumerable<TEntity> All(Guid? userId = null);
    Task<IEnumerable<TEntity>> AllAsync(Guid? userId = null);

    TEntity Find(Guid id, Guid? userId = null);
    Task<TEntity> FindAsync(Guid id, Guid? userId = null);

    void Add(TEntity entity);

    TEntity Update(TEntity entity);

    void Remove(TEntity entity, Guid? userId = null);

    void Remove(Guid id, Guid? userId = null);
}