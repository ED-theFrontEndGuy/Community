

using Microsoft.AspNetCore.Identity;

namespace Base.Interfaces;

public interface IDomainUser<TUser> : IDomainUser<Guid, TUser>
    where TUser : IdentityUser<Guid>
{
}

public interface IDomainUser<TKey, TUser>
    where TKey : IEquatable<TKey>
    where TUser : IdentityUser<TKey>
{
    TKey UserId { get; set; }
    TUser? User { get; set; }
}