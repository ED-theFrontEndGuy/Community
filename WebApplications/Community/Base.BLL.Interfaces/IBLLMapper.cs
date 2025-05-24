using Base.Interfaces;

namespace Base.BLL.Interfaces;

public interface IBLLMapper<TBllEntity, TDalEntity> : IBLLMapper<TBllEntity, TDalEntity, Guid>
    where TBllEntity : class, IDomainId
    where TDalEntity : class, IDomainId
{
}

public interface IBLLMapper<TBllEntity, TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainId<TKey> 
    where TBllEntity : class, IDomainId<TKey>
{
    public TBllEntity? Map(TDalEntity? entity);
    public TDalEntity? Map(TBllEntity? entity);
}