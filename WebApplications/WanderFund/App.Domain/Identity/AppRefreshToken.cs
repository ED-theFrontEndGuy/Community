using Base.Domain.Identity;
using Base.Interfaces;

namespace App.Domain.Identity;

public class AppRefreshToken : BaseRefreshToken, IDomainUserId
{
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
}