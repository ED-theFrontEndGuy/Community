using Base.Domain;

namespace App.Domain;

public class UserAchievement : BaseEntity
{
    public Guid AchievementId { get; set; }
    public Achievement? Achievement { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
}