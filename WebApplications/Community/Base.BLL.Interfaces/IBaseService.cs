using Base.Interfaces;
using Base.DAL.Interfaces;

namespace Base.BLL.Interfaces;

public interface IBaseService<TEntity> : IBaseService<TEntity, Guid>, IBaseRepository<TEntity>
    where TEntity : IDomainId
{
    
}

public interface IBaseService<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : IDomainId<TKey>
    where TKey : IEquatable<TKey>
{
    
}