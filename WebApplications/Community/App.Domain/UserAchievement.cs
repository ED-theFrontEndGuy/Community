using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class UserAchievement : BaseEntity
{
    public Guid AchievementId { get; set; }
    [Display(Name = nameof(Achievement), ResourceType = typeof(App.Resources.Domain.UserAchievement))]
    public Achievement? Achievement { get; set; }

    public Guid UserId { get; set; }
    [Display(Name = nameof(User), ResourceType = typeof(App.Resources.Domain.UserAchievement))]
    public User? User { get; set; }
}