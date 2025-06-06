namespace Base.Interfaces;

public interface IMapper<TSource, TTarget> : IMapper<TSource, TTarget, Guid>
    where TSource : class, IDomainId
    where TTarget : class, IDomainId
{
}

public interface IMapper<TSource, TTarget, TKey>
    where TKey : IEquatable<TKey>
    where TSource : class, IDomainId<TKey>
    where TTarget : class, IDomainId<TKey>
{
    public TSource? Map(TTarget? entity);
    public TTarget? Map(TSource? entity);
}