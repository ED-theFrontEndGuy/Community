using Base.Interfaces;

namespace Base.DAL.Interfaces;

public interface IUOWMapper<TDalEntity, TDomainEntity> : IUOWMapper<TDalEntity, TDomainEntity, Guid>
    where TDalEntity : class, IDomainId
    where TDomainEntity : class, IDomainId
{
}

public interface IUOWMapper<TDalEntity, TDomainEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainId<TKey> 
    where TDomainEntity : class, IDomainId<TKey>
{
    public TDalEntity? Map(TDomainEntity? entity);
    public TDomainEntity? Map(TDalEntity? entity);
}