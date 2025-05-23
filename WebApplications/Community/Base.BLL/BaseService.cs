using Base.BLL.Interfaces;
using Base.DAL.Interfaces;
using Base.Interfaces;

namespace Base.BLL;

public class BaseService<TBllEntity, TDalEntity> : BaseService<TBllEntity, TDalEntity, Guid>, IBaseService<TBllEntity>
    where TBllEntity : class, IDomainId
    where TDalEntity : class, IDomainId
{
    public BaseService(
            IBaseUOW serviceUOW,
            IBaseRepository<TDalEntity, Guid> serviceRepository, IBLLMapper<TBllEntity, TDalEntity, Guid> bllMapper
        ) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}

public class BaseService<TBllEntity, TDalEntity, TKey> : IBaseService<TBllEntity, TKey>
    where TBllEntity : class, IDomainId<TKey>
    where TDalEntity : class, IDomainId<TKey>
    where TKey : IEquatable<TKey>
{
    protected IBaseUOW ServiceUOW;
    protected IBaseRepository<TDalEntity, TKey> ServiceRepository ;
    protected IBLLMapper<TBllEntity, TDalEntity, TKey> BLLMapper;

    public BaseService(IBaseUOW serviceUOW, IBaseRepository<TDalEntity, TKey> serviceRepository, IBLLMapper<TBllEntity, TDalEntity, TKey> bllMapper)
    {
        ServiceUOW = serviceUOW;
        ServiceRepository = serviceRepository;
        BLLMapper = bllMapper;
    }

    public virtual IEnumerable<TBllEntity> All(TKey? userId = default)
    { 
        var entities = ServiceRepository.All(userId);
        return entities.Select(e => BLLMapper.Map(e)!).ToList();
    }

    public virtual async Task<IEnumerable<TBllEntity>> AllAsync(TKey? userId = default)
    {
        var entities = await ServiceRepository.AllAsync(userId);
        return entities.Select(e => BLLMapper.Map(e)!).ToList();
    }

    public virtual TBllEntity? Find(TKey id, TKey? userId = default)
    {
        var entity = ServiceRepository.Find(id, userId);
        return BLLMapper.Map(entity);
    }

    public virtual async Task<TBllEntity?> FindAsync(TKey id, TKey? userId = default)
    {
        var entity = await ServiceRepository.FindAsync(id, userId);
        return BLLMapper.Map(entity);
    }

    public virtual void Add(TBllEntity entity, TKey? userId = default)
    {
        var dalEntity = BLLMapper.Map(entity);
        ServiceRepository.Add(dalEntity!, userId);
    }

    public virtual TBllEntity Update(TBllEntity entity)
    {
        var dalEntity = BLLMapper.Map(entity);
        var updatedEntity = ServiceRepository.Update(dalEntity!);
        return BLLMapper.Map(updatedEntity)!;
    }

    public virtual void Remove(TBllEntity entity, TKey? userId = default)
    {
        ServiceRepository.Remove(BLLMapper.Map(entity)!, userId);
    }

    public virtual void Remove(TKey id, TKey? userId)
    {
        var entity = ServiceRepository.Find(id, userId);

        if (entity != null)
        {
            ServiceRepository.Remove(entity, userId);
        }
    }

    public virtual async Task RemoveAsync(TKey id, TKey? userId = default)
    {
        var entity = await ServiceRepository.FindAsync(id, userId);

        if (entity != null)
        {
            ServiceRepository.Remove(entity, userId);
        }
    }

    public virtual bool Exists(TKey id, TKey? userId = default)
    {
        var entity = ServiceRepository.Find(id, userId);
        return entity != null;
    }

    public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
    {
        var entity = await ServiceRepository.FindAsync(id, userId);
        return entity != null;
    }
}